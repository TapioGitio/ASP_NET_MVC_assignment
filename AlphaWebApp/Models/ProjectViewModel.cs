using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaWebApp.Models;

public class ProjectViewModel
{
    private readonly IMemberService _memberService;
    public ProjectViewModel(IMemberService memberService)
    {
        _memberService = memberService;
    }


    public AddProjectModel FormData { get; set; } = new();
    public EditProjectModel UpdateFormData { get; set; } = new();
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

}
