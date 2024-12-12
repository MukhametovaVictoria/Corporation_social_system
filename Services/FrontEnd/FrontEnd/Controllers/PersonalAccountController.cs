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
    public class PersonalAccountController : Controller
    {
        private readonly ILogger<PersonalAccountController> _logger;
        private readonly NewsService _newsService;
        private readonly PersonalAccountService _personalAccountService;
        private readonly AuthService _authService;

        public PersonalAccountController(ILogger<PersonalAccountController> logger, 
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

                InitDataHelper.InitViewData(ViewData, HttpContext, User?.Identity?.Name);
                var emoji = EmojiData.Emoji.All;
                var model = new List<EmojiViewModel>();
                foreach (var item in emoji)
                {
                    model.Add(new EmojiViewModel() { EmojiString = item.ToString(), Emoji = item, Hashcode = item.Sequence.GetHashCode() });
                }
                ViewData[Constants.EmojiViewModelListKey] = model;

                if(ViewData[Constants.PersonalAccountDataKey] == null)
                {
                    var userModel = await _personalAccountService.GetPersonalAccountData(HttpContext.Request.Cookies[Constants.UserIdCookieKey].ToString());
                    ViewData[Constants.PersonalAccountDataKey] = userModel;
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> UpdateInfo(UpdatingEmployeeModel employeeModel)
        {
            try
            {
                await _personalAccountService.Update(employeeModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
