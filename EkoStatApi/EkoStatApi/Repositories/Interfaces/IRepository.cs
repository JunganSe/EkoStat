using System.Linq.Expressions;

namespace EkoStatApi.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // Hämta
    Task<TEntity?> GetOnlyAsync(int id);
    Task<IEnumerable<TEntity>> GetAllOnlyAsync();
    Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> predicate, string? include = null);
    Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate = null, string? include = null);

    // Lägga till
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Ta bort
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
