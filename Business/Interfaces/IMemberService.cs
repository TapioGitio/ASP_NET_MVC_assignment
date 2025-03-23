using Domain.Models.DTO;
using Domain.Models.UpdateForms;
using System.Security.Claims;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetOneMemberAsync(ClaimsPrincipal user);
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<bool> UpdateMemberAsync(ClaimsPrincipal user, MemberUpdForm formData);
        Task<bool> DeleteAsync(ClaimsPrincipal user);
        
    }
}