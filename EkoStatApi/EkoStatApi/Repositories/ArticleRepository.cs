using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    public EkoStatContext EkoStatContext => (EkoStatContext)Context;
    public IQueryable<Article> ArticlesWithIncludes
        => EkoStatContext.Articles
            .Include(a => a.Entries)
            .Include(a => a.Tags);

    public ArticleRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Article?> GetAsync(int id)
    {
        return await ArticlesWithIncludes
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetByEntryAsync(int entryId)
    {
        return await ArticlesWithIncludes
            .Where(a => a.Entries.Any(e => e.Id == entryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByTagAsync(int tagId)
    {
        return await ArticlesWithIncludes
            .Where(a => a.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByUserAsync(int userId)
    {
        return await ArticlesWithIncludes
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}
