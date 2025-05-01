namespace Domain.Models;

public class Member
{
    public string Id { get; set; } = null!;
    public string? ProfileImagePath { get; set; }
    public string? JobTitle { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? FullName => $"{FirstName} {LastName}";

    public Address? Address { get; set; }

}
