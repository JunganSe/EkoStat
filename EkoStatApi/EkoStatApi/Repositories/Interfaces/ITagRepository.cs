using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

internal interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetAsync(int id);
    Task<IEnumerable<Tag>> GetByArticleAsync(int articleId);
    Task<IEnumerable<Tag>> GetByUserAsync(int userId);
}
