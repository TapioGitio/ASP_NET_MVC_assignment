using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }
    public bool IsCompleted { get; set; }


    public MemberEntity? Member { get; set; }
}
