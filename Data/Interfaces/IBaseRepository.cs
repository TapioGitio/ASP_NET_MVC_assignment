using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
}