using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

[Route("projects")]
public class ProjectController : Controller
{
    [Route("")]
    public IActionResult Projects()
    {
        return View();
    }

}
