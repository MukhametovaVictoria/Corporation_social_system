using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUser(User user, Register register);
        Task<User> LoginUser(string username, string password);
        Task<User?> ConfirmEmail(string email, string token);
        Task<bool> ChangeEmailAsync(string token, string oldEmail, string newEmail);
        Task<bool> ChangePasswordAsync(string userId, string newPassword, string oldPassword);
        Task<User?> RefreshToken(Token token);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> UpdateUser(User user);
    }
}
