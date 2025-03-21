using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaWebApp.Models;


public class ProjectViewModel
{
   
    private readonly IMemberService _memberService;
    private readonly IWebHostEnvironment _env;
    public ProjectViewModel(IMemberService memberService, IWebHostEnvironment env)
    {
        _memberService = memberService;
        _env = env;
    }


    public ProjectRegForm FormData { get; set; } = new();
    public ProjectUpdForm UpdateFormData { get; set; } = new();
    public List<SelectListItem> MemberOptions = [];


    public async Task LoadMembersAsync()
    {
        var members = await _memberService.GetMembers();
        MemberOptions = members.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.FullName
        }).ToList();
    }

    public async Task<string?> UploadImage()
    {
        try
        {
            if (FormData.ProjectImage == null)
                return null;

            else
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileName = Path.Combine(uploadFolder, Guid.NewGuid().ToString());

                using var stream = new FileStream(fileName, FileMode.Create);
                await FormData.ProjectImage.CopyToAsync(stream);

                
                return Path.Combine("uploads", fileName).Replace("\\", "/");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading image || {ex.Message}");
            return null;
        }
    } 
}

