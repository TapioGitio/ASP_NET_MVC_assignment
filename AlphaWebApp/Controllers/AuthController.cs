using AlphaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class AuthController(RegisterViewModel registerViewModel) : Controller
{
    private readonly RegisterViewModel _registerViewModel = registerViewModel;

    public IActionResult Login()
    {

        return View();
    }

    /*
    [Route("register")]
    public IActionResult Register()
    {
      
        return View(_registerViewModel);
    }


    [Route("register")]
    [HttpPost]
    public IActionResult Register(RegisterModel form)
    {
        if (!ModelState.IsValid || !form.AcceptTerms)
        {
            _registerViewModel.FormData = form;
            return View(_registerViewModel);
        }

        return RedirectToAction("Login");
    }*/
}
