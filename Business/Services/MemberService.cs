using Business.Factories;
using Business.Interfaces;
using Business.Models.DTO;
using Business.Models.UpdateForms;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<Member> GetOneMemberAsync(string Id)
    {
        try
        {
            var entity = await _memberRepository.GetOneAsync(x => x.Id == Id);
            if (entity == null)
                return null!;
            var member = MemberFactory.Create(entity);
            return member;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the member || {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        try
        {
            var entities = await _memberRepository.GetAllAsync();
            if (entities == null)
                return null!;

            var members = entities.Select(MemberFactory.Create);
            return members;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the members || {ex.Message}");
            return [];
        }
    }
    public async Task<bool> UpdateMemberAsync(string id, MemberUpdForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var entity = await _memberRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                return false;

            entity = MemberFactory.Update(entity, formData);
            var result = await _memberRepository.UpdateAsync(x => x.Id == id, entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not update the member || {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteAsync(string Id)
    {
        try
        {
            var entity = await _memberRepository.GetOneAsync(x => x.Id == Id);
            if (entity == null)
                return false;

            var result = await _memberRepository.DeleteAsync(x => x.Id == Id);

            return result;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Could not delete the member || {ex.Message}");
            return false;
        }
    }


}
