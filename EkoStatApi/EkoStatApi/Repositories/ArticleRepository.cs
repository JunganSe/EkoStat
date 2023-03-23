using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }

    public ArticleRepository(EkoStatContext context)
        : base(context)
    {
    }



    public Task<Article> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Article>> GetByUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Article>> GetByEntryAsync(int entryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Article>> GetByTagAsync(int entryId)
    {
        throw new NotImplementedException();
    }
}
