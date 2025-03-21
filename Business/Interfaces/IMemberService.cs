using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<bool> CreateMemberAsync(MemberRegForm formData);
        Task<bool> DeleteAsync(int Id);
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetOneMemberAsync(int Id);
        Task<bool> UpdateMemberAsync(int id, MemberUpdForm formData);
    }
}