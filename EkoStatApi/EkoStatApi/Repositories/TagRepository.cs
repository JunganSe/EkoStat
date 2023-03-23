using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }

    public TagRepository(EkoStatContext context)
        : base(context)
    {
    }



    public Task<Tag> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tag>> GetByUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
