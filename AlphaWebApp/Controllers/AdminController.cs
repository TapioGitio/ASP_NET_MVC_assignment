using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

[Authorize]
public class AdminController : Controller
{

    [Route("dashboard")]
    public IActionResult Index()
    {
        return View();
    }

}