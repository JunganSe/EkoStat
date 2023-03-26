using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IEntryRepository : IRepository<Entry>
{
    Task<Entry?> GetAsync(int id);
    Task<IEnumerable<Entry>> GetByArticleAsync(int articleId);
    Task<IEnumerable<Entry>> GetByTagAsync(int tagId);
    Task<IEnumerable<Entry>> GetByUserAsync(int userId);
}
