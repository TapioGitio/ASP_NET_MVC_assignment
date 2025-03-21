using Business.Factories;
using Business.Interfaces;
using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }
    public async Task<bool> CreateMemberAsync(MemberRegForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var duplicate = await _memberRepository.GetOneAsync(x => x.Email == formData.Email);
            if (duplicate != null)
                return false;

            var entity = MemberFactory.Create(formData);
            var result = await _memberRepository.CreateAsync(entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not create the member || {ex.Message}");
            return false;
        }
    }
    public async Task<Member> GetOneMemberAsync(int Id)
    {
        try
        {
            var entity = await _memberRepository.GetOneAsync(x => x.MemberId == Id);
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
            return new List<Member>();
        }
    }
    public async Task<bool> UpdateMemberAsync(int id, MemberUpdForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var entity = await _memberRepository.GetOneAsync(x => x.MemberId == id);
            if (entity == null)
                return false;

            entity = MemberFactory.Update(entity, formData);
            var result = await _memberRepository.UpdateAsync(x => x.MemberId == id, entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not update the member || {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteAsync(int Id)
    {
        try
        {
            var entity = await _memberRepository.GetOneAsync(x => x.MemberId == Id);
            if (entity == null)
                return false;

            var result = await _memberRepository.DeleteAsync(x => x.MemberId == Id);

            return result;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Could not delete the member || {ex.Message}");
            return false;
        }
    }
}