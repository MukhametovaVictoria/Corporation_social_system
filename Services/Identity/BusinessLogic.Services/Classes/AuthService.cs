using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly ISignInManagerWrapper _signInManagerWrapper;
        private readonly IUserManagerWrapper _userManagerWrapper;

        public AuthService(ISignInManagerWrapper signInManagerWrapper, IUserManagerWrapper userManagerWrapper)
        {
            _signInManagerWrapper = signInManagerWrapper;
            _userManagerWrapper = userManagerWrapper;
        }

        public async Task<bool> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userManagerWrapper.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return false;

            var result = await _signInManagerWrapper.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterUserDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Name = registerDto.Name
            };

            var result = await _userManagerWrapper.CreateAsync(user, registerDto.Password);
            return result.Succeeded;
        }

        public async Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(model.Login);
            var user2 = await _userManagerWrapper.FindByEmailAsync(model.Email);
            if (user == null || user?.Id != user2?.Id)//|| !(await _userManager.IsEmailConfirmedAsync(user))
            {
                return null;
            }

            var code = await _userManagerWrapper.GeneratePasswordResetTokenAsync(user);
            return new ForgotPasswordResponseDto() { Id = user.Id, Code = code };
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManagerWrapper.FindByIdAsync(model.UserId);
            var user2 = await _userManagerWrapper.FindByEmailAsync(model.Email);
            if (user == null || user?.Id != user2?.Id)// || !(await _userManager.IsEmailConfirmedAsync(user))
            {
                return null;
            }
            return await _userManagerWrapper.ResetPasswordAsync(user, model.Code, model.Password);
        }

        public async Task LogoutAsync()
        {
            await _signInManagerWrapper.SignOutAsync();
        }
    }
}