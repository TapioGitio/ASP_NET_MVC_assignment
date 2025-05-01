using AlphaWebApp.Models;
using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AlphaWebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MemberController(IMemberService memberService, IWebHostEnvironment env) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly IWebHostEnvironment _env = env;

    [Route("members")]
    public async Task<IActionResult> Index()
    {
        var model = new MemberViewModel
        {
            Members = await _memberService.GetMembersAsync()
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var member = await _memberService.GetOneMemberByIdAsync(id);
        var model = new MemberUpdForm
        {
            Id = member.Id,
            ProfileImagePath = member.ProfileImagePath,
            FirstName = member.FirstName!,
            LastName = member.LastName!,
            PhoneNumber = member.PhoneNumber,
            JobTitle = member.JobTitle,
            Street = member.Address?.Street,
            PostalCode = member.Address?.PostalCode,
            City = member.Address?.City,
        };
    
        return Json(new { memberData = model });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MemberUpdForm UpdateFormData)
    {
        var model = new MemberViewModel
        {
            UpdateFormData = UpdateFormData,
        };

        if (ModelState.IsValid)
        {
            var member = await _memberService.GetOneMemberByIdAsync(UpdateFormData.Id);
            if (member == null)
                return NotFound();

            UpdateFormData.ProfileImagePath = await UploadImageAsync(UpdateFormData.MemberImage);
            await _memberService.UpdateMemberAsync(UpdateFormData.Id, UpdateFormData);
            return RedirectToAction("Index");
        }
            return View(model);
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