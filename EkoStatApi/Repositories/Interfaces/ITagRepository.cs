using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetAsync(int id);
    Task<ICollection<Tag>> GetByArticleAsync(int articleId);
    Task<ICollection<Tag>> GetByUserAsync(int userId);
}
