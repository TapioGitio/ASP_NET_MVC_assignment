using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{

    public static ProjectRegForm Create() => new();

    public static ProjectEntity Create(ProjectRegForm formData) => new()
    {

        ProjectImagePath = formData.ProjectImagePath,
        ProjectName = formData.ProjectName,
        ClientName = formData.ClientName,
        ProjectDescription = formData.ProjectDescription,
        StartDate = DateTime.UtcNow.Date,
        EndDate = formData.EndDate,
        Budget = formData.Budget,
        MemberId = formData.MemberId,
        StatusId = formData.StatusId,

    };

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
        };
    }

    public static ProjectEntity Update(ProjectEntity entity, ProjectUpdForm formData)
    {
        return new ProjectEntity
        {
            Id = entity.Id,
            ProjectImagePath = formData.ProjectImagePath,
            ProjectName = formData.ProjectName,
            ClientName = formData.ClientName,
            ProjectDescription = formData.ProjectDescription,
            StartDate = entity.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            MemberId = formData.MemberId,
            StatusId = formData.StatusId

        };
    }

}
