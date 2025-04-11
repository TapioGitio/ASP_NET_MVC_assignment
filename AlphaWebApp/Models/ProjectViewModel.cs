using Domain.Models;

namespace AlphaWebApp.Models;

public class ProjectViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
    public int TotalProjects { get; set; }
    public int OngoingProjects { get; set; }
    public int CompletedProjects { get; set; }
    public List<string> SelectedMemberIds { get; set; } = []; 
    public ProjectRegForm FormData { get; set; } = new();
    public ProjectUpdForm UpdateFormData { get; set; } = new();   

}

