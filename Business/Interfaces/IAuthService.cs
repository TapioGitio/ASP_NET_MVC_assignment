using Domain.Models.RegForms;

namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(UserLoginForm form);
        Task<bool> RegisterAsync(UserRegistrationForm form);
        Task LogoutAsync();
    }
}