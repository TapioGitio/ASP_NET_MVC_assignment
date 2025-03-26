using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            return false;
        }

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not Create {nameof(TEntity)}, || {ex.Message}");
            return false;
        }
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);


            var result = await query.ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not retrieve a list of {nameof(TEntity)}, || {ex.Message}");
            return [];
        }
    }

    public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            return null;
        }

        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not retrieve a {nameof(TEntity)}, || {ex.Message}");
            return null;
        }
    }

    public virtual async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            Debug.WriteLine($"NewEntity is null || {nameof(TEntity)}");
            return false;
        }

        try
        {
            var BaseEntity = await _dbSet.FirstOrDefaultAsync(expression);

            if (BaseEntity == null)
            {
                Debug.WriteLine($"OldEntity is null || {nameof(TEntity)}");
                return false;
            }

            _context.Entry(BaseEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not Update {nameof(TEntity)}, || {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            Debug.WriteLine($"Expression is null || {nameof(TEntity)}");
            return false;
        }

        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
            if (entity == null)
            {
                Debug.WriteLine($"Entity is null || {nameof(TEntity)}");
                return false;
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not Delete {nameof(TEntity)}, || {ex.Message}");
            return false;
        }
    }
}
