using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AlphaWebApp.Models;

public class UserRegistrationForm
{
    [Display(Name = "First Name", Prompt = "Your first name")]
    [Required(ErrorMessage = "Required")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last Name", Prompt = "Your last name")]
    [Required(ErrorMessage = "Required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "Your email")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^(?!.*\.\.)[a-zA-Z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,50}$", ErrorMessage = "Invalid Email")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Atleast 8chars 1 uppercase 1 lowercase and 1 number")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-ö])(?=.*[A-Ö])(?=.*\d)[A-Öa-ö\d]{8,50}$", ErrorMessage = "Invalid format" )]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "Your password again..")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords doesn't match!")]
    public string ConfirmPassword { get; set; } = null!;


    [Required(ErrorMessage = "Required")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms")]
    public bool AcceptTerms { get; set; }
}
