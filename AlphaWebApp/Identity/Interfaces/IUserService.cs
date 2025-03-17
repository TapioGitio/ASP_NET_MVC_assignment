using AlphaWebApp.Models;

namespace AlphaWebApp.Identity.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateAsync(UserRegistrationForm form);
    }
}