using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<IEnumerable<ProjectEntity>> GetAllIncludeAllAsync();
        Task<ProjectEntity?> GetOneIncludeAllAsync(int Id);
    }
}