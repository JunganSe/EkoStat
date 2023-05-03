using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<ICollection<Tag>> GetByArticleAsync(int articleId);
    Task<ICollection<Tag>> GetByUserAsync(int userId);
    Task<ICollection<Tag>> GetByIdsAsync(List<int> tagIds);
}
