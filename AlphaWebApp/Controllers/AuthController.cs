using AlphaWebApp.Identity.Entity;
using AlphaWebApp.Identity.Interfaces;
using AlphaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class AuthController(IUserService userService, SignInManager<AppUser> signInManager) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;


    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationForm form)
    {

        if (!ModelState.IsValid)
            return View(form);

        var result = await _userService.CreateAsync(form);
        switch (result)
        {
            case 201:
                return RedirectToAction("Login", "Auth");
            case 400:
                ModelState.AddModelError("400", "Bad Request: some fields are invalid.");
                return View(form);
            case 409:
                ModelState.AddModelError("409", "Conflict: user already exists");
                return View(form);
            default:
                ModelState.AddModelError("500", "Internal Server Error");
                return View(form);
        }
    }

    [Route("login")]
    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginForm form)
    {

        if (!ModelState.IsValid)
        {
            ViewData["ErrorMessage"] = "Invalid email or password";
            return View(form);
        }


        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, false);
        if (result.Succeeded)
            return RedirectToAction("Projects", "Project");
        else
            return View(form);
    }
}
