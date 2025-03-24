using Domain.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegForm formData);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetOneProjectIncludeAllAsync(int Id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdForm formData);
        Task<bool> DeleteProjectAsync(int id);
    }
}