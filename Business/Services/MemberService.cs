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

    public async Task<Member> GetOneMemberByIdAsync(string id)
    {
        try
        {
            var entity = await _userManager.FindByIdAsync(id);
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

    public async Task<string> GetMemberNameAsync(string? memberName)
    {
        if (memberName == null)
            return "";

        var member = await _userManager.FindByNameAsync(memberName);
        return member == null ? "" : $"{member.FirstName} {member.LastName}";
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

    public async Task<IEnumerable<Member>> GetMembersAsync(string term)
    {
        try
        {
            var entities = await _userManager.Users
                .Where(u =>
                    u.FirstName!.ToLower().Contains(term.ToLower()) ||
                    u.LastName!.ToLower().Contains(term.ToLower()) ||
                    (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(term.ToLower()))
                .ToListAsync();

            return entities.Select(MemberFactory.Create);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not search members || {ex.Message}");
            return [];
        }
    }
    public async Task<bool> UpdateMemberAsync(string id, MemberUpdForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var entity = await _userManager.FindByIdAsync(id);
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
   
