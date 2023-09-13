using System.Linq.Expressions;

namespace ArmstrongServer.Interfaces;

interface IRepository<TEntity> where TEntity : class
{
  Task<TEntity> GetByIdAsync(int id);
  Task<IEnumerable<TEntity>> GetAllAsync();
  Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
  Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
  Task AddAsync(TEntity entity);
  Task AddRangeAsync(IEnumerable<TEntity> entities);
  void Update(TEntity entity);
  void UpdateRange(IEnumerable<TEntity> entities);
  void Remove(TEntity entity);
  void RemoveRange(IEnumerable<TEntity> entities);
}
