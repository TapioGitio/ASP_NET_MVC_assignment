using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaWebApp.Models;


public class ProjectViewModel
{
   



    public AddProjectModel FormData { get; set; } = new();
    public EditProjectModel UpdateFormData { get; set; } = new();
    public List<SelectListItem> MemberOptions = [];


    public async Task LoadMembersAsync()
    {
   

    }
    
}

