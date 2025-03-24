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
            IsCompleted = formData.StatusBool,
           
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
            StatusBool = entity.IsCompleted,
        };
    }

    public static ProjectEntity Update(ProjectEntity entity, ProjectUpdForm updForm)
    {
        return new ProjectEntity
        {
            Id = entity.Id,
            ProjectImagePath = updForm.ProjectImagePath,
            ProjectName = updForm.ProjectName,
            ClientName = updForm.ClientName,
            ProjectDescription = updForm.ProjectDescription,
            StartDate = entity.StartDate,
            EndDate = updForm.EndDate,
            Budget = updForm.Budget,
            IsCompleted = updForm.StatusBool,
        };
    }
}
