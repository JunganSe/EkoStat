using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

internal interface IArticleRepository : IRepository<Article>
{
    Task<Article?> GetAsync(int id);
    Task<IEnumerable<Article>> GetByEntryAsync(int entryId);
    Task<IEnumerable<Article>> GetByTagAsync(int tagId);
    Task<IEnumerable<Article>> GetByUserAsync(int userId);
}
