﻿using System.ComponentModel.DataAnnotations;

namespace AlphaWebApp.Models;

public class AddProjectModel
{
    public int ProjectId { get; set; }
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


    [Display(Name = "Start Date", Prompt = "Start date")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Required")]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date", Prompt = "End date")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Required")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Members")]
    public int MemberId { get; set; }

    [Display(Name = "Budget")]
    [Required(ErrorMessage = "Required")]
    public decimal Budget { get; set; }

}
