using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.Timesheet;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;

namespace FrontEnd.Controllers
{
	[Authorize]
	public class TimesheetController : Controller
	{
		private readonly ILogger<TimesheetController> _logger;
		private readonly PersonalAccountService _personalAccountService;
		private readonly TimesheetService _timesheetService;
		private readonly AuthService _authService;

        public TimesheetController(ILogger<TimesheetController> logger,
			PersonalAccountService personalAccountService,
			TimesheetService timesheetService,
			AuthService authService)
		{
			_logger = logger;
			_personalAccountService = personalAccountService;
			_timesheetService = timesheetService;
			_authService = authService;
		}

        public async Task<IActionResult> Index(DateOnly start, DateOnly till)
		{
			try
			{
				var dataStr = HttpContext.Session.GetString(Constants.TimeSheetDataKey);
				var data = dataStr != null ? JsonSerializer.Deserialize<TimesheetViewModel>(dataStr) : null;
                var needFullPrepare = data == null || data.CurrentStartDate == default || data.CurrentTillDate == default;
				if (needFullPrepare)
				{
					var model = await FullPrepare(start, till, ViewData);
					ViewData[Constants.TimeSheetDataKey] = model;
                    HttpContext.Session.SetString(Constants.TimeSheetDataKey, JsonSerializer.Serialize(model));
                }
				else
				{
                    InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);
                    if (start == default)
                        start = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Monday));

                    if (till == default)
                        till = start.AddDays(6);

                    data.CurrentStartDate = start;
                    data.CurrentTillDate = till;
					data.FirstStart = start.AddDays(-7 * 3);
					data.End = till.AddDays(7 * 3);
                    if(data.FirstStart < data.MaxStart)
                    {
                        data.FirstStart = data.MaxStart;
                        data.End = data.FirstStart.AddDays((7 * 7)-1);
                    }
                    if(data.End > data.MaxEnd)
                    {
                        data.End = data.MaxEnd;
                        data.FirstStart = data.MaxEnd.AddDays((-7*7)+1);
                    }
                        
					var projGuids = new List<Guid>();
                    if (data.TimesheetData != null && data.TimesheetData.Count > 0)
                    {
						var current = new List<List<TimeTrackerModel>>();
						foreach (var dataPerProject in data.TimesheetData)
						{
							var list = dataPerProject.Where(x => x.Date >= start && x.Date <= till).OrderBy(x => x.Date).ToList();
							if(list.Count > 0)
							{
                                current.Add(list);
                                projGuids.Add(list[0].ProjectId);
                            }
                        }
                        data.CurrentTimesheetData = current;
						data.Projects = current.Count > 0 ? data.AllProjects.Where(x => !projGuids.Contains(x.Id)).ToList() : data.AllProjects;
                    }
                    else
                    {
                        data.Projects = data.AllProjects;
                    }
                    ViewData[Constants.TimeSheetDataKey] = data;
                    HttpContext.Session.SetString(Constants.TimeSheetDataKey, JsonSerializer.Serialize(data));
                }
				return View();
			}
			catch (Exception ex)
			{
				return View();
			}
		}

        [HttpPost]
		public async Task<IActionResult> CreateTimesheet(Guid projectId, DateOnly start, DateOnly till)
		{
			try
			{
				var employeeId = HttpContext.Request.Cookies[Constants.UserIdCookieKey];
				if (employeeId != null && Guid.TryParse(employeeId, out var guid) && projectId != Guid.Empty && start != default && till != default)
				{
					var list = new List<CreatingTimeTrackerModel>();
					var rangeDateTime = till.AddDays(1).ToDateTime(new TimeOnly(0,0,0)) - start.ToDateTime(new TimeOnly(0, 0, 0));
					var range = (int)rangeDateTime.TotalDays;
					var days = range > 7 ? 7 : range;
					for(var i = 0; i < days; i++)
					{
						var date = days-1 == i && days == range ? till : start.AddDays(i);
						var filter = new CreatingTimeTrackerModel()
						{
							EmployeeId = guid,
							ProjectId = projectId,
							Date = date
						};
						list.Add(filter);
					}
                    var timesheetData = await _timesheetService.CreateTimesheetData(list);

                    var dataStr = HttpContext.Session.GetString(Constants.TimeSheetDataKey);
                    var data = dataStr != null ? JsonSerializer.Deserialize<TimesheetViewModel>(dataStr) : null;
					if (data != null && timesheetData != null && timesheetData.Count > 0) //We can update page without requests to service
					{
                        data.CurrentTimesheetData.Add(timesheetData.OrderBy(x => x.Date).ToList());
                        var wasAdded = false;
                        foreach (var a in data.TimesheetData)
                        {
                            if (a != null && a.Count > 0 && a[0].ProjectId == projectId)
                            {
                                a.AddRange(timesheetData);
                                wasAdded = true;
                                break;
                            }
                        }
                        if (!wasAdded)
                            data.TimesheetData.Add(timesheetData);
                        HttpContext.Session.SetString(Constants.TimeSheetDataKey, JsonSerializer.Serialize(data));
                    }
                }

				return RedirectToAction("Index", new {start, till});
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index", new { start, till });
			}
		}

		public async Task<IActionResult> Update(Guid id, DateOnly start, DateOnly till, int time)
		{
			try
			{
				if (id != Guid.Empty)
				{
					var model = new UpdatingTimeTrackerModel()
					{
						TimeAtWork = time
					};

					var result = await _timesheetService.Update(id, model);
                    if (result)
                    {
                        var dataStr = HttpContext.Session.GetString(Constants.TimeSheetDataKey);
                        var data = dataStr != null ? JsonSerializer.Deserialize<TimesheetViewModel>(dataStr) : null;
                        if (data != null) //We can update page without requests to service
                        {
                            foreach (var item in data.TimesheetData)
                            {
                                var val = item.FirstOrDefault(x => x.Id == id);
                                if (val != null)
                                {
                                    val.TimeAtWork = time;
                                }
                            }

                            HttpContext.Session.SetString(Constants.TimeSheetDataKey, JsonSerializer.Serialize(data));
                        }
                    }
                }

                return RedirectToAction("Index", new { start, till });
            }
			catch (Exception ex)
			{
                return RedirectToAction("Index", new { start, till });
            }
		}

		public async Task<IActionResult> Delete(DateOnly start, DateOnly till, List<Guid> ids)
		{
			try
			{
				if (ids != null && ids.Count > 0)
				{
					await _timesheetService.Delete(ids);
                    var dataStr = HttpContext.Session.GetString(Constants.TimeSheetDataKey);
                    var data = dataStr != null ? JsonSerializer.Deserialize<TimesheetViewModel>(dataStr) : null;
					if(data != null)
					{
                        for (var i = 0; i < data.TimesheetData.Count; i++)
                        {
                            var items = data.TimesheetData[i].Where(x => ids.Contains(x.Id)).ToList();
                            if (items.Count > 0)
                            {
                                foreach (var item in items)
                                {
                                    data.TimesheetData[i].Remove(item);
                                }
                                break;
                            }
                        }
                        for (var i = 0; i < data.CurrentTimesheetData.Count; i++)
                        {
                            var items = data.CurrentTimesheetData[i].Where(x => ids.Contains(x.Id)).ToList();
                            if (items.Count > 0)
                            {
                                foreach (var item in items)
                                {
                                    data.CurrentTimesheetData[i].Remove(item);
                                }
                                
                                break;
                            }
                        }
                        HttpContext.Session.SetString(Constants.TimeSheetDataKey, JsonSerializer.Serialize(data));
                    }
                }

                return RedirectToAction("Index", new { start, till });
            }
			catch (Exception ex)
			{
                return RedirectToAction("Index", new { start, till });
            }
		}

        [HttpPost]
        public async Task<ActionResult> CreateProject(CreatingProjectModel model)
        {
            try
            {
                var result = await _timesheetService.CreateProject(model);
                if(result != Guid.Empty)
                {
                    if (HttpContext.Session.GetString(Constants.TimeSheetDataKey) != null)
                        HttpContext.Session.Remove(Constants.TimeSheetDataKey);
                }
            }
            catch (Exception ex) { }

            return RedirectToAction("Projects");
        }

        [HttpGet]
        public ActionResult CreateProject()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);
            ViewData[Constants.IsAdminKey] = HttpContext?.Request?.Cookies[User?.Identity?.Name + Constants.IsAdminPrefix];
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Projects()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);
            ViewData[Constants.IsAdminKey] = HttpContext?.Request?.Cookies[User?.Identity?.Name + Constants.IsAdminPrefix];
            ViewData[Constants.ProjectsDataKey] = await _timesheetService.GetProjects(); ;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private void PrepareViewData(TimesheetViewModel model, DateOnly start, DateOnly till, CaptionsBase langs)
		{
            if (langs == null)
                langs = new CaptionsBase();
            model.WeekDays = new List<string>()
                {
                    langs.MondayShortCaption,
                    langs.TuesdayShortCaption,
                    langs.WednesdayShortCaption, 
                    langs.ThursdayShortCaption,
                    langs.FridayShortCaption,
                    langs.SaturdayShortCaption,
                    langs.SundayShortCaption
                };

            model.Months = new Dictionary<int, string>
                {
                    {1, langs.JanShortCaption}, {2, langs.FebShortCaption},{3, langs.MarchShortCaption},{4, langs.AprShortCaption},
                    {5, langs.MayShortCaption},{6, langs.JuneShortCaption},{7, langs.JuleShortCaption},{8, langs.AugShortCaption},
                    {9, langs.SepShortCaption},{10, langs.OctShortCaption},{11, langs.NovShortCaption},{12, langs.DecShortCaption}
                };
        }

		private async Task<TimesheetViewModel> FullPrepare(DateOnly start, DateOnly till, ViewDataDictionary viewData)
		{
            var model = new TimesheetViewModel();

            if (start == default)
                start = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Monday));

            if (till == default)
                till = start.AddDays(6);

            model.CurrentStartDate = start;
            model.CurrentTillDate = till;

            await InitDataHelper.InitSession(HttpContext, _personalAccountService, _authService, viewData, User?.Identity?.Name);
            InitDataHelper.InitViewData(viewData, HttpContext, User?.Identity?.Name);

            var lang = Constants.LanguageBase;
            if (HttpContext?.Request?.Cookies[User.Identity.Name + Constants.LanguagePrefix] != null)
                lang = HttpContext?.Request?.Cookies[User.Identity.Name + Constants.LanguagePrefix].ToString();

            PrepareViewData(model, start, till, (CaptionsBase)Constants.Dictionaries[lang]);

            var employeeId = HttpContext.Request.Cookies[Constants.UserIdCookieKey];
            var projectsGuids = new List<Guid>();
            if (employeeId != null && Guid.TryParse(employeeId, out var guid))
            {
                var filter = new TimeTrackerFilterModel()
                {
                    EmployeeId = guid,
                    StartDate = start.AddDays(-7 * 31),
                    TillDate = till.AddDays(7 * 31),
                    ProjectId = Guid.Empty
                };
                var timesheetData = await _timesheetService.GetTimesheetData(filter);
                var list = new List<List<TimeTrackerModel>>();
                var current = new List<List<TimeTrackerModel>>();
                if (timesheetData != null && timesheetData.Count > 0)
                {
                    projectsGuids = timesheetData.OrderBy(x => x.ProjectId).Select(x => x.ProjectId).Distinct().ToList();

                    foreach (var project in projectsGuids)
                    {
                        list.Add(timesheetData.Where(x => x.ProjectId == project).OrderBy(x => x.Date).ToList());
                        current.Add(timesheetData.Where(x => x.ProjectId == project && x.Date >= start && x.Date <= till).OrderBy(x => x.Date).ToList());
                    }
                }
                model.TimesheetData = list;
                model.CurrentTimesheetData = current;
            }

            var projects = await _timesheetService.GetProjects();
            model.AllProjects = projects?.OrderBy(x => x.Name).ToList();
            model.Projects = projects?.Where(x => !projectsGuids.Contains(x.Id)).OrderBy(x => x.Name).ToList();
            model.FirstStart = start.AddDays(-7 * 3);
            model.End = till.AddDays(7 * 3);
			model.MaxStart = start.AddDays(-7 * 31);
			model.MaxEnd = till.AddDays(7 * 31);
            return model;
		}
	}
}
