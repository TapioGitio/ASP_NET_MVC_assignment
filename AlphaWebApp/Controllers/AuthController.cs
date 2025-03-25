using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;


    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginForm form)
    {

        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            if (result)
                return RedirectToAction("Index", "Admin");
        }

        ViewBag.ErrorMessage = "Invalid email or password";
        return View(form);
    }

    [Route("Register")]
    public IActionResult Register()
    {

        return View();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserRegistrationForm form)
    {

        if (ModelState.IsValid)
        {
            var result = await _authService.RegisterAsync(form);
            if (result)
                return LocalRedirect("~/");
            else
            {
                ViewBag.ErrorMessage = "User with same email already exists";
                return View(form);
            }
        }
       
        return View(form);
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Login", "Auth");
    }

    [Route("accessdenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

}
