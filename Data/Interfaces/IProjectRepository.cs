using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity?> GetOneIncludeAllAsync(int Id);
    }
}