using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AlphaWebApp.Identity.Entity;

public class AppUser : IdentityUser
{
    [ProtectedPersonalData]
    [Display(Name = "First Name", Prompt = "Your first name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    [Display(Name = "Last Name", Prompt = "Your last name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;


    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms")]
    public bool AcceptTerms { get; set; }
}
