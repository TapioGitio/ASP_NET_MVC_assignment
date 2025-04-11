using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public async Task<IEnumerable<ProjectEntity>> GetAllIncludeAllAsync()
    {
        var result = await _dbSet
            .Include(x => x.Members)
            .ToListAsync();
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
