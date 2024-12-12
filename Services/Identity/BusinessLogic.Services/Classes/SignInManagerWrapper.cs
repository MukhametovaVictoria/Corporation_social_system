using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Classes;

public class SignInManagerWrapper : ISignInManagerWrapper
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignInManagerWrapper(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        => _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

    public Task SignOutAsync() => _signInManager.SignOutAsync();
}