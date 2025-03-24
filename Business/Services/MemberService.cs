using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<Member> GetOneMemberAsync(ClaimsPrincipal user)
    {
        try
        {
            var entity = await _userManager.GetUserAsync(user);
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
    
    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        try
        {
            var entities = await _userManager.Users.ToListAsync();
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
    public async Task<bool> UpdateMemberAsync(ClaimsPrincipal user, MemberUpdForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var entity = await _userManager.GetUserAsync(user);
            if (entity == null)
                return false;

            entity = MemberFactory.Update(entity, formData);
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not update the member || {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteAsync(ClaimsPrincipal user)
    {
        try
        {
            var entity = await _userManager.GetUserAsync(user);
            if (entity == null)
                return false;

            var result = await _userManager.DeleteAsync(entity);

            return result.Succeeded;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Could not delete the member || {ex.Message}");
            return false;
        }
    }


}
   
