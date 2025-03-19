using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectEntity
{

    public int Id { get; set; }

    public IFormFile? ProjectImage { get; set; }

    public string ProjectName { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string ProjectDescription { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Budget { get; set; }

    public int MemberId { get; set; }
    public MemberEntity Member { get; set; } = null!;
}



    



