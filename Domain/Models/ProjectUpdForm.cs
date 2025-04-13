using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class ProjectUpdForm
{
    public int Id { get; set; }

    public string? ProjectImagePath { get; set; }
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    public string? ProjectName { get; set; }


    [Display(Name = "Client Name", Prompt = "Client name")]
    public string? ClientName { get; set; }


    [Display(Name = "Description", Prompt = "Type something")]
    public string? ProjectDescription { get; set; }


    [Display(Name = "Start Date", Prompt = "Start date")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date", Prompt = "End date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }


    [Display(Name = "Budget")]
    public decimal? Budget { get; set; }

    [Display(Name = "Mark if project is completed")]
    public bool IsCompleted { get; set; }


    [Display(Name = "Members")]
    public string? SelectedMemberIdsRaw { get; set; }

    [NotMapped]
    public List<string> SelectedMemberIds =>
        SelectedMemberIdsRaw?
        .Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToList() ?? new();

}
