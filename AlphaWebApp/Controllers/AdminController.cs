using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }
}
