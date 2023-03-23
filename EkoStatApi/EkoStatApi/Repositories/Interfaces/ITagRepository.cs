using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag> GetAsync(int id);
    Task<IEnumerable<Tag>> GetByUserAsync(int userId);
}
