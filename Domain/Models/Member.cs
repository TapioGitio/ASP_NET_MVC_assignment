using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class Member
{
    public string MemberId { get; set; } = null!;
    public IFormFile? MemberImage { get; set; }
    public string? ProfileImagePath { get; set; }
    public string? JobTitle { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? FullName => $"{FirstName} {LastName}";

    public Address? Address { get; set; }

}
