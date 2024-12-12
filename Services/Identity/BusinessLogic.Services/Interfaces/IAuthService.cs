using BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginDto);
        Task<bool> RegisterAsync(RegisterUserDto registerDto);
        Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordDto model);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task LogoutAsync();
    }
}