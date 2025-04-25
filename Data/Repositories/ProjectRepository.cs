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
        // got some help with the syntax for this in order to be able
        // to use the predicate parameter, for the listing of projects
        // that are completed or not and to include the members as in the second method.
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
