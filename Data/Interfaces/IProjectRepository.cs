using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<IEnumerable<ProjectEntity>> GetAllIncludeAllAsync(Expression<Func<ProjectEntity, bool>>? predicate = null);
        Task<ProjectEntity?> GetOneIncludeAllAsync(int Id);
    }
}