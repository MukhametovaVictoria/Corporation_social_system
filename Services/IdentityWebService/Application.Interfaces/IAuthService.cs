using Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterDto registerDto);

        Task<UserDto> LoginUser(string username, string password);

        Task<UserDto?> ConfirmEmail(string email, string token);

        Task<bool> ChangePasswordAsync(string userId, string newPassword, string oldPassword);

        Task<UserDto> RefreshToken(TokenDto tokenDto);

        Task<bool> SendEmailChangeMessageAsync(string userId, string newEmail);

        Task<bool> ChangeEmailAsync(string token, string newEmail, string oldEmail);
    }
}
