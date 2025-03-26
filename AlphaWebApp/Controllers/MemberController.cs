using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MemberController : Controller
{

  
    [Route("members")]
    public IActionResult Index()
    {
        return View();
    }
}
