using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace AlphaWebApp.Models;

public class ProjectViewModel
{
    public AddProjectModel FormData { get; set; } = new();

    public EditProjectModel UpdateFormData { get; set; } = new();

}
