using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{

    public async Task<IEnumerable<ProjectEntity>> GetAllIncludeAllAsync(Expression<Func<ProjectEntity, bool>>? predicate = null)
    {
        IQueryable<ProjectEntity> query = _dbSet
            .Include(x => x.Members);

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<ProjectEntity?> GetOneIncludeAllAsync(int Id)
    {
        var result = await _dbSet
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == Id);

        return result;
    }
}
