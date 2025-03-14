using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApp.Controllers
{
    public class ProjectCRUDController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
