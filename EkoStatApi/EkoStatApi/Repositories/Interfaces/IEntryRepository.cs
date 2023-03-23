using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IEntryRepository : IRepository<Entry>
{
    Task<Entry> GetAsync(int id);
    Task<IEnumerable<Entry>> GetByUserAsync(int userId);
    Task<IEnumerable<Entry>> GetByArticleAsync(int articleId);
}
