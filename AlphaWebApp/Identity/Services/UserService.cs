using AlphaWebApp.Identity.Entity;
using AlphaWebApp.Identity.Interfaces;
using AlphaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlphaWebApp.Identity.Services;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IUserService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<int> CreateAsync(UserRegistrationForm form)
    {
        if (form == null)
            return 400;

        var duplicate = await _userManager.Users.AnyAsync(x => x.Email == form.Email);
        if (duplicate == true)
            return 409;


        var user = new AppUser
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            UserName = form.Email
        };

        var result = await _userManager.CreateAsync(user, form.Password);
        if (result.Succeeded)
            return 201;
        else
            return 500;
    }
}
