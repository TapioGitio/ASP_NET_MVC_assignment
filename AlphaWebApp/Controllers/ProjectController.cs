using AlphaWebApp.Models;
using Domain.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;

namespace AlphaWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _env;

        public ProjectController(IProjectService projectService, IMemberService memberService, IWebHostEnvironment env)
        {
            _projectService = projectService;
            _memberService = memberService;
            _env = env;
        }

        [Route("projects")]
        public async Task<IActionResult> Projects()
        {
            var model = new ProjectViewModel
            {
                Projects = await _projectService.GetAllProjects(),
                FormData = new ProjectRegForm(),
                UpdateFormData = new ProjectUpdForm(),
                MemberOptions = (await _memberService.GetMembersAsync())
                    .Select(x => new SelectListItem
                    {
                        Value = x.MemberId.ToString(),
                        Text = x.FullName,
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectRegForm formData)
        {
            var model = new ProjectViewModel
            {
                FormData = formData,
                MemberOptions = (await _memberService.GetMembersAsync())
                    .Select(x => new SelectListItem
                    {
                        Value = x.MemberId.ToString(),
                        Text = x.FullName,
                    }).ToList()
            };

            if (!ModelState.IsValid)
                return View(model);


            
            formData.ProjectImagePath = await UploadImageAsync(formData);
            await _projectService.CreateProjectAsync(formData);

            return RedirectToAction("Projects");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Edit(ProjectUpdForm updForm)
        {
            var model = new ProjectViewModel
            {
                UpdateFormData = updForm,
                MemberOptions = (await _memberService.GetMembersAsync())
                    .Select(x => new SelectListItem
                    {
                        Value = x.MemberId.ToString(),
                        Text = x.FullName,
                    }).ToList()
            };

            if (!ModelState.IsValid)
                return View(model);
            

            updForm.ProjectImagePath = await UploadImageAsync(updForm);
            await _projectService.UpdateProjectAsync(updForm.Id, updForm);

            return RedirectToAction("Projects");
   
        }



        public async Task<string?> UploadImageAsync(ProjectRegForm formData)
        {
            return await HandleUploadImageAsync(formData.ProjectImage!);
        }

        public async Task<string?> UploadImageAsync(ProjectUpdForm updform)
        {
            return await HandleUploadImageAsync(updform.ProjectImage!);
        }

        public async Task<string?> HandleUploadImageAsync(IFormFile image)
        {
            try
            {

                if (image == null)
                    return null;

      
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileExtension = Path.GetExtension(image.FileName);
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return Path.Combine("uploads", fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image || {ex.Message}");
                return null;
            }
        }
    }
}