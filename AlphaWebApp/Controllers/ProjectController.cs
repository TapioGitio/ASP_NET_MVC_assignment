using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class ProjectController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
