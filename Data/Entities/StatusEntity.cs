using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class StatusEntity
{
    [Key]
    public int StatusId { get; set; }

    public bool Status { get; set; }
    public ICollection<ProjectEntity> Projects { get; set; } = null!;
}
