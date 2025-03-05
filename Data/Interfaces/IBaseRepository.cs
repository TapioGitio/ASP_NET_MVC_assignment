using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> Create(TEntity entity);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetOne(Expression<Func<TEntity, bool>> expression);
    Task<bool> Update(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
    Task<bool> Delete(Expression<Func<TEntity, bool>> expression);
}