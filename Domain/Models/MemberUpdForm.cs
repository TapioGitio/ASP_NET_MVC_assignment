using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class MemberUpdForm
{
    public string Id { get; set; } = null!;
    public IFormFile? MemberImage { get; set; }
    public string? ProfileImagePath { get; set; }

    [Display(Name = "Job Title", Prompt = "Job title")]
    [Required(ErrorMessage = "Required")]
    public string? JobTitle { get; set; }

    [Display(Name = "First Name", Prompt = "First name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Last name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Phone Number", Prompt = "Phone Number")]
    public string? PhoneNumber { get; set; }
    public bool AcceptTerms { get; set; }



    [Display(Name = "Street", Prompt = "Street")]
    public string? Street { get; set; }

    [Display(Name = "Zip Code", Prompt = "Zip Code")]
    public string? ZipCode { get; set; }

    [Display(Name = "City", Prompt = "City")]
    public string? City { get; set; }
}
