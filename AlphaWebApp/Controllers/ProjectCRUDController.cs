using AlphaWebApp.Models;
using Domain.Models.RegForms;
using Microsoft.AspNetCore.Mvc;


namespace AlphaWebApp.Controllers
{
    public class ProjectCRUDController(ProjectViewModel projectViewModel) : Controller
    {
        private readonly ProjectViewModel _projectViewModel = projectViewModel;

        public async Task<IActionResult> Add()
        {
            await _projectViewModel.LoadMembersAsync();
            return View(_projectViewModel);
        }

        [HttpPost]
        public async Task <IActionResult> Add(ProjectRegForm formData)
        {

            if (!ModelState.IsValid)
            {
                _projectViewModel.FormData = formData;
                return View(_projectViewModel);
            }

            formData.ProjectImagePath = await _projectViewModel.UploadImage();
          
            //await _projectService.CreateAsync(formData);
            //upload image

            return RedirectToAction("Projects");
        }

        public IActionResult Edit()
        {
            return View();
        }

    }
}

