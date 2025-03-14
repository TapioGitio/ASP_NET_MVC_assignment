using System.ComponentModel.DataAnnotations;

namespace AlphaWebApp.Models;

public class AddProjectModel
{
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;


    [Display(Name = "Client Name", Prompt = "Client name")]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;


    [Display(Name = "Description", Prompt = "Type something")]
    [Required(ErrorMessage = "Required")]
    public string ProjectDescription { get; set; } = null!;


    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int MemberId { get; set; }

    [Display(Name = "Budget")]
    public decimal Budget { get; set; }

}
