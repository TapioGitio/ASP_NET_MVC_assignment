﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ProjectUpdForm
{
    public int Id { get; set; }

    public string? ProjectImagePath { get; set; }
    public IFormFile? ProjectImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Project name")]
    [Required(ErrorMessage = "Please edit or quit")]
    public string ProjectName { get; set; } = null!;


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

    [Display(Name = "Members")]
    public int? MemberId { get; set; }

    [Display(Name = "Budget")]
    public decimal? Budget { get; set; }

    [Display(Name = "Mark if project is completed")]
    public bool IsCompleted { get; set; }
}
