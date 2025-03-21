using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegForm formData);
        Task<Project> GetOneProjectIncludeAllAsync(int Id);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<bool> UpdateProjectAsync(int id, ProjectUpdForm formData);
        Task<bool> DeleteProjectAsync(int id);
    }
}