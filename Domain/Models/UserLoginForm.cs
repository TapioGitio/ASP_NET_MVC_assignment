using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UserLoginForm
{
    [Display(Name = "Email", Prompt = "Your email")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Your password")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}
