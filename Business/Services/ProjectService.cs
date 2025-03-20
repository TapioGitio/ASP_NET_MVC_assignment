using Business.Models.RegForms;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task CreateAsync(ProjectRegForm formData)
    {
    
    }
}
