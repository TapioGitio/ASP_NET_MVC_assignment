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

    [HttpPost]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MemberUpdForm UpdateFormData)
    {
        var model = new MemberViewModel
        {
            UpdateFormData = UpdateFormData,
        };

        if (!ModelState.IsValid) 
            return View(model);


        UpdateFormData.ProfileImagePath = await UploadImageAsync(UpdateFormData);
        await _memberService.UpdateMemberAsync(UpdateFormData.Id, UpdateFormData);
        return RedirectToAction("Index");
    }



    public async Task<string?> UploadImageAsync(MemberUpdForm UpdateFormData)
    {
        return await HandleUploadImageAsync(UpdateFormData.MemberImage!);
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
