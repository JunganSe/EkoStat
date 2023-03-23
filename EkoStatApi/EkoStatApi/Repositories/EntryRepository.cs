using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class EntryRepository : Repository<Entry>, IEntryRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }

    public EntryRepository(EkoStatContext context)
        : base(context)
    {
    }



    public Task<Entry> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entry>> GetByUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entry>> GetByArticleAsync(int articleId)
    {
        throw new NotImplementedException();
    }
}
