using Data.Entities;
using Domain.Models;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegForm Create() => new();

    public static ProjectEntity Create(ProjectRegForm formData)
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
        return new ProjectEntity
        {
            Id = entity.Id,
            ProjectImagePath = string.IsNullOrEmpty(updForm.ProjectImagePath) ? entity.ProjectImagePath : updForm.ProjectImagePath,
            ProjectName = string.IsNullOrEmpty(updForm.ProjectName) ? entity.ProjectName : updForm.ProjectName,
            ClientName = string.IsNullOrEmpty(updForm.ClientName) ? entity.ClientName : updForm.ClientName,
            ProjectDescription = string.IsNullOrEmpty(updForm.ProjectDescription) ? entity.ProjectDescription : updForm.ProjectDescription,
            StartDate = updForm.StartDate ?? entity.StartDate,
            EndDate = updForm.EndDate ?? entity.EndDate,
            Budget = updForm.Budget ?? entity.Budget,
            IsCompleted = updForm.IsCompleted,
        };
    }
}
