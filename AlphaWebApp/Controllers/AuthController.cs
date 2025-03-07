using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }
}
