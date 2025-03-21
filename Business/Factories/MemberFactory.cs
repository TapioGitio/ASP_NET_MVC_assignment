using Business.Models.DTO;
using Business.Models.UpdateForms;
using Data.Entities;
using Domain.Models;

namespace Business.Factories;

public static class MemberFactory
{
    public static UserRegistrationForm Create() => new();
    
    public static MemberEntity Create(UserRegistrationForm form)
    {
        return new MemberEntity
        {
            UserName = form.Email,
            ProfileImagePath = form.ProfileImagePath,
            JobTitle = form.JobTitle,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            PhoneNumber = form.PhoneNumber,

            AcceptTerms = form.AcceptTerms
        };
    }

    public static Member Create(MemberEntity entity)
    {
        return new Member
        {
            MemberId = entity.Id,
            ProfileImagePath = entity.ProfileImagePath,
            JobTitle = entity.JobTitle,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
       
        };
    }

    public static MemberEntity Update(MemberEntity entity, MemberUpdForm form)
    {
        return new MemberEntity
        {
            Id = entity.Id,
            ProfileImagePath = form.ProfileImagePath,
            JobTitle = form.JobTitle,
            FirstName = form.FirstName,
            LastName = form.LastName,
            PhoneNumber = form.PhoneNumber,
            AcceptTerms = entity.AcceptTerms

        };
    }
}
