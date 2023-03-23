using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IArticleRepository : IRepository<Article>
{
    Task<Article> GetAsync(int id);
    Task<IEnumerable<Article>> GetByUserAsync(int userId);
    Task<IEnumerable<Article>> GetByEntryAsync(int entryId);
    Task<IEnumerable<Article>> GetByTagAsync(int entryId);
}
