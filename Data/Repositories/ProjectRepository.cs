using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public async Task<ProjectEntity?> GetOneIncludeAllAsync(int Id)
    {
        var result = await _dbSet
            .Include(x => x.Member)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == Id);

        return result;
    }
}
