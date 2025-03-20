﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{

    [Key]
    public int Id { get; set; }

    public string? ProjectImagePath { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string ProjectName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string ClientName { get; set; } = null!;

    [Column(TypeName = "nvarchar(200)")]
    public string ProjectDescription { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Budget { get; set; }


    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    public int? MemberId { get; set; }
    public MemberEntity? Member { get; set; }
}



    



