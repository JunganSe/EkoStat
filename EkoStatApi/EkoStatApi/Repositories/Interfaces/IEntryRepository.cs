using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IEntryRepository : IRepository<Entry>
{
    Task<Entry?> GetAsync(int id);
    Task<ICollection<Entry>> GetByArticleAsync(int articleId);
    Task<ICollection<Entry>> GetByTagAsync(int tagId);
    Task<ICollection<Entry>> GetByUserAsync(int userId);
}
