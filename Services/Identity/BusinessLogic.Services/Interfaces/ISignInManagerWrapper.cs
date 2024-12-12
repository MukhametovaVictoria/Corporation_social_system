using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Interfaces;

public interface ISignInManagerWrapper
{
    Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
    Task SignOutAsync();
}