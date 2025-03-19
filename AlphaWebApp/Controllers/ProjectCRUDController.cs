using AlphaWebApp.Models;
using Business.Models.RegForms;
using Microsoft.AspNetCore.Mvc;


namespace AlphaWebApp.Controllers
{
    public class ProjectCRUDController(ProjectViewModel projectViewModel) : Controller
    {
        private readonly ProjectViewModel _projectViewModel = projectViewModel;
        
        public async Task<IActionResult> Add()
        {
            /*await _projectViewModel.LoadMembersAsync();*/
            return View(_projectViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddProjectModel formData)
        {

            if (!ModelState.IsValid)
            {
                _projectViewModel.FormData = formData;
                return View(_projectViewModel);   
            }
    
            return RedirectToAction("Projects");
        }

        public IActionResult Edit()
        {
            return View();
        }
        
    }
}

