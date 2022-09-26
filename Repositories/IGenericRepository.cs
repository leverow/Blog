using System.Linq.Expressions;

namespace Blog.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? GetById(ulong id);
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask AddRange(IEnumerable<TEntity> entities);
    ValueTask<TEntity> Remove(TEntity entity);
    ValueTask RemoveRange(IEnumerable<TEntity> entities);
    ValueTask<TEntity> Update(TEntity entity);
}