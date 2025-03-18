using AlphaWebApp.Identity.Interfaces;
using AlphaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class AuthController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;


    public IActionResult Login()
    {

        return View();
    }


    [Route("register")]
    public IActionResult Register()
    {

        return View();
    }


    [Route("register")]
    [HttpPost]
    public async Task <IActionResult> Register(UserRegistrationForm form)
    {
        
        if (!ModelState.IsValid || !form.AcceptTerms)
        {
            return View(form);
        }

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
}
