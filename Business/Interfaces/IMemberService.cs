using Domain.Models;
using System.Security.Claims;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetOneMemberAsync(ClaimsPrincipal user);
        Task<Member> GetOneMemberByIdAsync(string id);
        Task<string> GetMemberNameAsync(string? memberName);
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<bool> UpdateMemberAsync(string id, MemberUpdForm formData);
        Task<bool> DeleteAsync(ClaimsPrincipal user);
    }
}