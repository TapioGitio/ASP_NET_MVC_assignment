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

        };
    }

    public static Member Create(MemberEntity entity)
    {
        return new Member
        {
            Id = entity.Id,
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


        entity.ProfileImagePath = form.ProfileImagePath;
        entity.JobTitle = form.JobTitle;
        entity.FirstName = form.FirstName;
        entity.LastName = form.LastName;
        entity.PhoneNumber = form.PhoneNumber;

        // Got help from ai, to be able to set and check if the address is null or not,
        // and to be able to set the address to null if all fields are empty.

        if (!string.IsNullOrWhiteSpace(form.Street) ||
            !string.IsNullOrWhiteSpace(form.PostalCode) ||
            !string.IsNullOrWhiteSpace(form.City))
        {
            if (entity.Address == null)
                entity.Address = new AddressEntity();

            if (!string.IsNullOrWhiteSpace(form.Street))
                entity.Address.Street = form.Street;

            if (!string.IsNullOrWhiteSpace(form.PostalCode))
                entity.Address.PostalCode = form.PostalCode;

            if (!string.IsNullOrWhiteSpace(form.City))
                entity.Address.City = form.City;

        }
        else
            entity.Address = null;


        return entity;

 
    }
}
