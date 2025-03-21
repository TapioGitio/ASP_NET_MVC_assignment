using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

//[Authorize]
public class AdminController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [Route("projects")]
    public IActionResult Projects()
    {
        return View();
    }

    //[Authorize(Roles = "admin")]
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }
}
