using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIdentity.Models;

namespace TestIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request");

            var loginSuccess = await _authService.LoginAsync(loginDto);
            if (!loginSuccess)
            {
                return Unauthorized(new UserModel() { IsFound = false });
            }

            return RedirectToAction("GetUserByName", "Users", new { name = loginDto.UserName });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid registration request");

            var registerSuccess = await _authService.RegisterAsync(registerDto);
            if (!registerSuccess)
                return BadRequest("Registration failed");

            return RedirectToAction("SendMessageAndGetUserByName", "Users", new { name = registerDto.UserName });
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var result = await _authService.ForgotPassword(new ForgotPasswordDto() { Email = model.Email, Login = model.Login });
            if(result != null)
                return Ok(new ForgotPasswordResponseModel() { Id = result.Id, Code = result.Code });
            return Ok(null);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel model)
        {
            var result = await _authService.ResetPassword(new ResetPasswordDto() { 
                Email = model.Email, UserId = model.UserId, Code = model.Code, ConfirmPassword = model.ConfirmPassword, Password = model.Password
            });
            var errors = new List<string>();
            if(result != null)
            {
                foreach(var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
            }
            return Ok(new ResetPasswordResponseModel() { Succeeded = result != null ? result.Succeeded : false, Errors = errors });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok("Logout successful");
        }
    }
}