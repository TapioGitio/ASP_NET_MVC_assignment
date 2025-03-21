using Business.Models.DTO;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetOneMemberAsync(string Id);
        Task<bool> UpdateMemberAsync(string id, MemberUpdForm formData);
        Task<bool> DeleteAsync(string Id);
    }
}