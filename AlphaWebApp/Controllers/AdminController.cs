using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

//[Authorize]
public class AdminController : Controller  
{

    [Route("dashboard")]
    public IActionResult Index()
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
