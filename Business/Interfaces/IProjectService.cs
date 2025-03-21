using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegForm formData);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetOneProjectIncludeAllAsync(int Id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdForm formData);
    }
}