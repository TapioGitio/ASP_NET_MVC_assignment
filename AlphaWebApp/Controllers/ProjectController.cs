﻿using AlphaWebApp.Models;
using Domain.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlphaWebApp.Controllers
{
    [Authorize]
    public class ProjectController(IProjectService projectService, IWebHostEnvironment env) : Controller
    {
        private readonly IProjectService _projectService = projectService;
        private readonly IWebHostEnvironment _env = env;

        [Route("projects")]
        public async Task<IActionResult> Index()
        {
            var model = new ProjectViewModel
            {
                Projects = await _projectService.GetAllProjects(),
                TotalProjects = (await _projectService.GetAllProjects()).Count(),
                OngoingProjects = (await _projectService.GetOngoingProjects()).Count(),
                CompletedProjects = (await _projectService.GetCompletedProjects()).Count(),
            };
            return View(model);
        }

        [Route("projects/ongoing")]
        public async Task<IActionResult> Ongoing()
        {
            var model = new ProjectViewModel
            {
                Projects = await _projectService.GetOngoingProjects(),
                TotalProjects = (await _projectService.GetAllProjects()).Count(),
                OngoingProjects = (await _projectService.GetOngoingProjects()).Count(),
                CompletedProjects = (await _projectService.GetCompletedProjects()).Count(),
            };
            return View("Index", model);
        }

        [Route("projects/completed")]
        public async Task<IActionResult> Completed()
        {
            var model = new ProjectViewModel
            {
                Projects = await _projectService.GetCompletedProjects(),
                TotalProjects = (await _projectService.GetAllProjects()).Count(),
                OngoingProjects = (await _projectService.GetOngoingProjects()).Count(),
                CompletedProjects = (await _projectService.GetCompletedProjects()).Count(),
            };
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectRegForm formData)
        {
            var model = new ProjectViewModel
            {
                FormData = formData,
            };

            if (!ModelState.IsValid)
                return View(model);

            formData.ProjectImagePath = await UploadImageAsync(formData.ProjectImage);
            await _projectService.CreateProjectAsync(formData, formData.SelectedMemberIds);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectUpdForm UpdateFormData)
        {
            var model = new ProjectViewModel
            {
                UpdateFormData = UpdateFormData,
            };

            if (!ModelState.IsValid)
                return View(model);


            UpdateFormData.ProjectImagePath = await UploadImageAsync(UpdateFormData.ProjectImage);
            await _projectService.UpdateProjectAsync(UpdateFormData.Id, UpdateFormData, UpdateFormData.SelectedMemberIds);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(ProjectUpdForm UpdateFormData)
        {
            await _projectService.DeleteProjectAsync(UpdateFormData.Id);
            return RedirectToAction("Index");
        }


        private async Task<string?> UploadImageAsync(IFormFile? image)
        {
            if (image == null) return null;
            return await HandleUploadImageAsync(image);
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