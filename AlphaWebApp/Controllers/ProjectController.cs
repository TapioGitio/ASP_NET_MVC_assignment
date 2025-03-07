using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

public class ProjectController : Controller
{
    [Route("projects")]
    public IActionResult Index()
    {
        return View();
    }
}
