using System.Linq.Expressions;

namespace EkoStatApi.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // Hämta
    Task<TEntity?> GetMinimalAsync(int id);
    Task<ICollection<TEntity>> GetAllMinimalAsync();
    Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> predicate, string? include = null);
    Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate = null, string? include = null);

    // Lägga till
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(ICollection<TEntity> entities);

    // Ta bort
    void Remove(TEntity entity);
    void RemoveRange(ICollection<TEntity> entities);
}
