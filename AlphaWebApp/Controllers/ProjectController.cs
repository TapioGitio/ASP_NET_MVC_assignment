using AlphaWebApp.Models;
using Domain.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Route("projects/add")]
        [HttpPost]
        public async Task<IActionResult> Add(ProjectRegForm formData)
        {
            var model = new ProjectViewModel
            {
                Projects = await _projectService.GetAllProjects(),
                FormData = formData,
                MemberOptions = (await _memberService.GetMembersAsync())
                    .Select(x => new SelectListItem
                    {
                        Value = x.MemberId.ToString(),
                        Text = x.FullName,
                    }).ToList()
            };

            if (!ModelState.IsValid)
            {
                return View("Projects", model);
            }

            formData.ProjectImagePath = await UploadImageAsync(formData);
            await _projectService.CreateProjectAsync(formData);

            return RedirectToAction("Projects");
        }

        public IActionResult Edit()
        {
            return View();
        }


        public async Task<string?> UploadImageAsync(ProjectRegForm formData)
        {
            try
            {

                if (formData.ProjectImage == null)
                    return null;

      
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.ProjectImage.CopyToAsync(stream);
                }

                return Path.Combine("uploads", fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return null;
            }
        }
    }
}