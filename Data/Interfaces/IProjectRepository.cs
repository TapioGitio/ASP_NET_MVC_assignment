using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task <ProjectEntity?> ReadOneIncludeAllAsync(int Id);
}
