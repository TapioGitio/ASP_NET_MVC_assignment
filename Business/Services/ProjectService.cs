/*using Business.Factories;
using Business.Interfaces;
using Business.Models.DTO;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegForm formData)
    {

        try
        {
            if (formData == null)
                return false;

            var duplicate = await _projectRepository.GetOneAsync(x => x.ProjectName == formData.ProjectName);

            if (duplicate != null)
                return false;

            var entity = ProjectFactory.Create(formData);
            var result = await _projectRepository.CreateAsync(entity);

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not create the project || {ex.Message}");
            return false;
        }

    }

    public async Task<Project> GetOneProjectIncludeAllAsync(int Id)
    {
        try
        {
            var entity = await _projectRepository.GetOneIncludeAllAsync(Id);

            if (entity == null)
                return null!;

            var project = ProjectFactory.Create(entity);

            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the project || {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        try
        {
            var entities = await _projectRepository.GetAllAsync();
            if (entities == null)
                return null!;

            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not read the projects || {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdForm formData)
    {
        try
        {
            if (formData == null)
                return false;

            var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                return false;

            var updatedEntity = ProjectFactory.Update(entity, formData);

            var result = await _projectRepository.UpdateAsync(x => x.Id == id, updatedEntity);

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not update the project || {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                return false;

            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not delete the project || {ex.Message}");
            return false;
        }
    }
}
*/