using Data.Entities;
using Domain.Models;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegForm Create() => new();

    public static ProjectEntity Create(ProjectRegForm formData, List<MemberEntity> members)
    {
        return new ProjectEntity
        {
            ProjectImagePath = formData.ProjectImagePath,
            ProjectName = formData.ProjectName,
            ClientName = formData.ClientName,
            ProjectDescription = formData.ProjectDescription,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            IsCompleted = formData.IsCompleted,
            Members = members
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            ProjectImagePath = entity.ProjectImagePath,
            ProjectName = entity.ProjectName,
            ClientName = entity.ClientName,
            ProjectDescription = entity.ProjectDescription,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            IsCompleted = entity.IsCompleted,
            Members = entity.Members.Select(m => new Member
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                ProfileImagePath = m.ProfileImagePath
            }).ToList()
            //Got help from ChatGPT to add this part
            // to convert members from MemberEntity to Member
            // and to add ProfileImagePath.
        };
    }

    public static ProjectEntity Update(ProjectEntity entity, ProjectUpdForm updForm, List<MemberEntity> members)
    {

        entity.ProjectImagePath = string.IsNullOrEmpty(updForm.ProjectImagePath) ? entity.ProjectImagePath : updForm.ProjectImagePath;
        entity.ProjectName = updForm.ProjectName ?? entity.ProjectName;
        entity.ClientName = updForm.ClientName ?? entity.ClientName;
        entity.ProjectDescription = updForm.ProjectDescription ?? entity.ProjectDescription;
        entity.StartDate = updForm.StartDate ?? entity.StartDate;
        entity.EndDate = updForm.EndDate ?? entity.EndDate;
        entity.Budget = updForm.Budget ?? entity.Budget;
        entity.IsCompleted = updForm.IsCompleted;


        // And here to be able to update members
        // Its supposed to check if theres exisiting members
        // and if not, add them to the list
        // It doesnt work yet, but I will try to fix it.
        if (members != null)
        {
            var newMemberIds = members.Select(m => m.Id).ToHashSet();

            entity.Members = entity.Members
                .Where(m => newMemberIds.Contains(m.Id))
                .ToList();

            var existingMemberIds = entity.Members.Select(m => m.Id).ToHashSet();
            var membersToAdd = members.Where(m => !existingMemberIds.Contains(m.Id)).ToList();

            foreach (var member in membersToAdd)
            {
                entity.Members.Add(member);
            }
        }
        return entity;
    }
}
