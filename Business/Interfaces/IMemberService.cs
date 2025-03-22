using Domain.Models.DTO;
using Domain.Models.UpdateForms;
using System.Security.Claims;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<bool> DeleteAsync(string Id);
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetOneMemberAsync(ClaimsPrincipal user);
        Task<bool> UpdateMemberAsync(string id, MemberUpdForm formData);
    }
}