using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.Auth;
using FrontEnd.Models.EmployeeService;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            if (User.Identity != default && User.Identity.IsAuthenticated)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetString(User.Identity.Name)))
                    return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Login/Login.cshtml");
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
                var user = await _authService.AuthenticateAsync(username, password);

                if (user != null)
                {
                    if (user.IsFound)
                    {
                        HttpContext.Session.SetString(username, user.Id.ToString());
                        HttpContext.Session.SetString(username+Constants.FullNamePrefix, user.Name);
                        HttpContext.Response.Cookies.Append(Constants.UserIdCookieKey, user.Id.ToString(), new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddMinutes(30)
                        });
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorMsg = string.Empty;
                    if(ViewData[Constants.CaptionsKey] != null)
                    {
                        errorMsg = ((CaptionsBase)ViewData[Constants.CaptionsKey])?.BadAuthDataCaption;
                    }
                    else
                    {
                        var lang = new CaptionsBase();
                        errorMsg = lang.BadAuthDataCaption;
                    }
                    ViewBag.ErrorMessage = errorMsg;
                    return View("~/Views/Login/Login.cshtml");
                }
            }
            catch (HttpRequestException)
            {
                var errorMsg = string.Empty;
                if (ViewData[Constants.CaptionsKey] != null)
                {
                    errorMsg = ((CaptionsBase)ViewData[Constants.CaptionsKey]).AuthServiceIsNotAllowedCaption;
                }
                else
                {
                    var lang = new CaptionsBase();
                    errorMsg = lang.AuthServiceIsNotAllowedCaption;
                }
                ViewBag.ErrorMessage = errorMsg;
                return View("~/Views/Login/Login.cshtml");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            return View("~/Views/Login/Register.cshtml");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterSuccess()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginProblem(string name, string email, string userName, string content)
        {
            try
            {
                InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
                var newModel = new LoginProblemRequest() { Email = email, UserName = name, Login = userName, Message = content };
                _authService.SendProblemRequest(newModel);
                return View("LoginProblemConfirmation");
            }
            catch(Exception ex)
            {

            }
            
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginProblem()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(string name, string email, string office)
        {
            try
            {
                InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
                var newUser = new NewUserModel()
                {
                    Name = name,
                    Email = email,
                    OfficeAddress = office
                };
                _authService.SendRegisterRequest(newUser);

                return RedirectToAction("RegisterSuccess", "Login");
            }
            catch (HttpRequestException)
            {
                return View("~/Views/Login/Register.cshtml");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            if (ModelState.IsValid)
            {
                var user = await _authService.ForgotPassword(model);
                if(user != null)
                {
                    var callbackUrl = Url.Action("ResetPassword", "Login", new { userId = user.Id, code = user.Code }, protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Reset Password",
                        $"<div>Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a></div>");
                    return View("ForgotPasswordConfirmation");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId = null, string code = null)
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            return code == null || userId == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            InitDataHelper.InitViewData(ViewData, HttpContext, "Default");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _authService.ResetPassword(model);
            if (result != null && result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            else if(result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                return View("ResetPasswordConfirmation");
            }
            
            return View(model);
        }

        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.Request.Cookies[Constants.UserIdCookieKey];

            //_authService.LogoutAsync(userId);

            HttpContext.Response.Cookies.Delete(Constants.UserIdCookieKey);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
