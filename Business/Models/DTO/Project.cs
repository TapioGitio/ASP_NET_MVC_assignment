﻿namespace Business.Models.DTO;

public class Project
{
    public int Id { get; set; }
    public string? ProjectImagePath { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }

}
