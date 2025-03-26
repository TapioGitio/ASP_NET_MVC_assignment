using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaWebApp.Models;

public class ProjectViewModel
{
    public IEnumerable<Project> Projects = [];
    public int TotalProjects { get; set; }
    public int OngoingProjects { get; set; }
    public int CompletedProjects { get; set; }
    public List<SelectListItem> MemberOptions = [];
    public ProjectRegForm FormData { get; set; } = new();
    public ProjectUpdForm UpdateFormData { get; set; } = new();   

}

