using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Classes;

public class UserManagerWrapper : IUserManagerWrapper
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManagerWrapper(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<ApplicationUser?> FindByIdAsync(string userId) => _userManager.FindByIdAsync(userId);
    public Task<ApplicationUser?> FindByNameAsync(string userName) => _userManager.FindByNameAsync(userName);
    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) => _userManager.CreateAsync(user, password);
    public Task<IdentityResult> UpdateAsync(ApplicationUser user) => _userManager.UpdateAsync(user);
    public Task<IdentityResult> DeleteAsync(ApplicationUser user) => _userManager.DeleteAsync(user);
    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => _userManager.CheckPasswordAsync(user, password);
    public Task<ApplicationUser?> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);
    public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user) => _userManager.GeneratePasswordResetTokenAsync(user);
    public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password) => _userManager.ResetPasswordAsync(user, code, password);
}