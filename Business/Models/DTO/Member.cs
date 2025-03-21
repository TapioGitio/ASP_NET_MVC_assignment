using Microsoft.AspNetCore.Http;

namespace Business.Models.DTO;

public class Member
{
    public int MemberId { get; set; }
    public IFormFile? MemberImage { get; set; }
    public string? ProfileImagePath { get; set; }
    public string JobTitle { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string FullName => $"{FirstName} {LastName}";


    public int? AddressId { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
}
