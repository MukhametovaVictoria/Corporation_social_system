using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.PersonalAccountModels.Employee;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsService _newsService;
        private readonly PersonalAccountService _personalAccountService;
        private readonly AuthService _authService;

        public HomeController(ILogger<HomeController> logger, 
            NewsService newsService, 
            PersonalAccountService personalAccountService,
            AuthService authService)
        {
            _logger = logger;
            _newsService = newsService;
            _personalAccountService = personalAccountService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await InitDataHelper.InitSession(HttpContext, _personalAccountService, _authService, ViewData, User?.Identity?.Name);
                InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);

                if (Guid.TryParse(HttpContext.Session.GetString(User.Identity.Name), out var userId))
                {
                    try
                    {
                        var newsList = await _newsService.GetPublishedListAsync(1, 10, userId);
                        ViewData[Constants.NewsFeedListViewDataKey] = newsList;
                    }
                    catch (Exception ex) { }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like(string newsId)
        {
            try
            {
                if (!string.IsNullOrEmpty(newsId) && Guid.TryParse(newsId, out var id))
                {
                    if (Guid.TryParse(HttpContext.Session.GetString(User.Identity.Name), out var userId))
                    {
                        var likesInfo = await _newsService.Like(id, userId);
                        return Ok(likesInfo);
                    }
                }
            }
            catch (Exception ex) { }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SetLang(string language)
        {
            try
            {
                if(!string.IsNullOrEmpty(language) && language != HttpContext.Session.GetString(User.Identity.Name + Constants.LanguagePrefix) && Constants.Dictionaries.ContainsKey(language))
                {
                    HttpContext.Session.SetString(User.Identity.Name + Constants.LanguagePrefix, language);
                    
                    if (HttpContext?.Request?.Cookies[User.Identity.Name + Constants.LanguagePrefix] != null)
                        HttpContext.Response.Cookies.Delete(User.Identity.Name + Constants.LanguagePrefix);

                    HttpContext?.Response?.Cookies.Append(User.Identity.Name + Constants.LanguagePrefix, language, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddDays(30)
                    });

                    InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);

                    if (Guid.TryParse(HttpContext.Session.GetString(User.Identity.Name), out var userId))
                    {
                        try
                        {
                            var empModel = await _personalAccountService.GetPersonalAccountData(userId.ToString());
                            if (empModel != null)
                            {
                                var newModel = new UpdatingEmployeeModel()
                                {
                                    Id = empModel.Id,
                                    Firstname = empModel.Firstname,
                                    Surname = empModel.Surname,
                                    Position = empModel.Position,
                                    MainEmail = empModel.MainEmail,
                                    MainTelephoneNumber = empModel.MainTelephoneNumber,
                                    About = empModel.About,
                                    Birthdate = empModel.Birthdate == null ? DateTime.MinValue : (DateTime)empModel.Birthdate,
                                    OfficeAddress = empModel.OfficeAddress,
                                    EmploymentDate = empModel.EmploymentDate == null ? DateTime.MinValue : (DateTime)empModel.EmploymentDate,
                                    IsAdmin = empModel.IsAdmin,
                                    IsDeleted = empModel.IsDeleted,
                                    Language = language
                                };
                                empModel.Language = language;
                                await _personalAccountService.Update(newModel);

                                ViewData[Constants.PersonalAccountDataKey] = empModel;

                                if(HttpContext.Session.GetString(Constants.TimeSheetDataKey) != null)
                                    HttpContext.Session.Remove(Constants.TimeSheetDataKey);
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            InitDataHelper.InitSession(HttpContext, _personalAccountService, _authService, ViewData, User?.Identity?.Name);
            InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
