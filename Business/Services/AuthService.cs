using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Domain.Models.RegForms;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> LoginAsync(UserLoginForm form)
    {
        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, false);
        return result.Succeeded;
    }

    public async Task<bool> RegisterAsync(UserRegistrationForm form)
    {
        var entity = MemberFactory.Create(form);

        var result = await _userManager.CreateAsync(entity, form.Password);
        return result.Succeeded;
    }
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
