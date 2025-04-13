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

        };
    }

    public static ProjectEntity Update(ProjectEntity entity, ProjectUpdForm updForm)
    {

        entity.ProjectImagePath = string.IsNullOrEmpty(updForm.ProjectImagePath) ? entity.ProjectImagePath : updForm.ProjectImagePath;
        entity.ProjectName = string.IsNullOrEmpty(updForm.ProjectName) ? entity.ProjectName : updForm.ProjectName;
        entity.ClientName = string.IsNullOrEmpty(updForm.ClientName) ? entity.ClientName : updForm.ClientName;
        entity.ProjectDescription = string.IsNullOrEmpty(updForm.ProjectDescription) ? entity.ProjectDescription : updForm.ProjectDescription;
        entity.StartDate = updForm.StartDate ?? entity.StartDate;
        entity.EndDate = updForm.EndDate ?? entity.EndDate;
        entity.Budget = updForm.Budget ?? entity.Budget;
        entity.IsCompleted = updForm.IsCompleted;

        return entity;
    }
}
