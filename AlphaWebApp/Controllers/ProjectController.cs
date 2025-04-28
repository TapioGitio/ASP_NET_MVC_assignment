using AlphaWebApp.Models;
using Domain.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlphaWebApp.Hubs;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace AlphaWebApp.Controllers
{
    [Authorize]
    public class ProjectController(IProjectService projectService, IWebHostEnvironment env, INotificationService notificationService, IHubContext<NotificationHub> notificationHub, UserManager<MemberEntity> userManager) : Controller
    {
        private readonly IProjectService _projectService = projectService;
        private readonly UserManager<MemberEntity> _userManager = userManager;
        private readonly INotificationService _notificationService = notificationService;
        private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
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
            var result = await _projectService.CreateProjectAsync(formData, formData.SelectedMemberIds);
            if (result)
            {

                /* Send notification */
                var project = (await _projectService.GetAllProjects())
                    .FirstOrDefault(x => x.ProjectName == formData.ProjectName);

                if (project != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        var notify = new NotificationEntity
                        {
                            Image = project.ProjectImagePath!,
                            Message = $"{project.ProjectName} added",
                            NotificationTypeId = 2,
                        };

                        await _notificationService.AddNotificationAsync(notify);
                        var notifications = await _notificationService.GetNotificationsAsync(user.Id);
                        var newNotification = notifications.OrderByDescending(x => x.Created).FirstOrDefault();

                        if (newNotification != null)
                        {
                            await _notificationHub.Clients.All.SendAsync("RecieveNotification", newNotification);
                        }
                    }

                    return RedirectToAction("Index");
                }

            }
                return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetOneProjectIncludeAllAsync(id);
            if (project == null)
                return NotFound();

            var model = new ProjectUpdForm
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                ProjectDescription = project.ProjectDescription,
                ProjectImagePath = project.ProjectImagePath,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                IsCompleted = project.IsCompleted,
                SelectedMemberIdsRaw = string.Join((","), project.Members.Select(m => m.Id))
            };


            return Json(new { updateFormData = model }); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectUpdForm UpdateFormData)
        {
            var model = new ProjectViewModel
            {
                UpdateFormData = UpdateFormData,
            };

            if (ModelState.IsValid)
            {
                var project = await _projectService.GetOneProjectIncludeAllAsync(UpdateFormData.Id);
                if (project == null)
                    return NotFound();


                // Parse the new member IDs from the form
                var newMembers = UpdateFormData.SelectedMemberIdsRaw?
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => id.Trim())
                    .ToList() ?? [];

                // Merge existing and new members
                var oldMembers = project.Members.Select(m => m.Id).ToList();
                var combinedMembers = oldMembers.Concat(newMembers).ToList();

                UpdateFormData.ProjectImagePath = await UploadImageAsync(UpdateFormData.ProjectImage);
                await _projectService.UpdateProjectAsync(UpdateFormData.Id, UpdateFormData, combinedMembers);
                return RedirectToAction("Index");

            }
                return View(model);
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