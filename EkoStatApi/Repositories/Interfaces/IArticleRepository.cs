using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IArticleRepository : IRepository<Article>
{
    Task<Article?> GetAsync(int id);
    Task<ICollection<Article>> GetByEntryAsync(int entryId);
    Task<ICollection<Article>> GetByTagAsync(int tagId);
    Task<ICollection<Article>> GetByUserAsync(int userId);
}
