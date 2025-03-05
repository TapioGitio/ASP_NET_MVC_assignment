﻿using Data.Context;
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

    public virtual async Task<bool> Create(TEntity entity)
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
    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        try
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not retrieve a list of {nameof(TEntity)}, || {ex.Message}");
            return [];
        }
    }

    public virtual async Task<TEntity?> GetOne(Expression<Func<TEntity, bool>> expression)
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

    public virtual async Task<bool> Update(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            Debug.WriteLine($"NewEntity is null || {nameof(TEntity)}");
            return false;
        }

        try
        {
            var BaseEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;

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

    public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> expression)
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
