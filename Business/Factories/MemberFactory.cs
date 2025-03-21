using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class MemberFactory
{
    public static MemberRegForm Create() => new();

    public static MemberEntity Create(MemberRegForm formData) => new()
    {
        ProfileImagePath = formData.ProfileImagePath,
        JobTitle = formData.JobTitle,
        FirstName = formData.FirstName,
        LastName = formData.LastName,
        Email = formData.Email,
        PhoneNumber = formData.PhoneNumber,
        AddressId = formData.AddressId,
    };

    public static Member Create(MemberEntity entity)
    {
        return new Member
        {
            MemberId = entity.MemberId,
            ProfileImagePath = entity.ProfileImagePath,
            JobTitle = entity.JobTitle,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            AddressId = entity.AddressId,
        };
    }

    public static MemberEntity Update(MemberEntity entity, MemberUpdForm formData)
    {
        return new MemberEntity
        {
            MemberId = entity.MemberId,
            ProfileImagePath = formData.ProfileImagePath,
            JobTitle = formData.JobTitle,
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
            PhoneNumber = formData.PhoneNumber,
            AddressId = formData.AddressId,
        };
    }
}
