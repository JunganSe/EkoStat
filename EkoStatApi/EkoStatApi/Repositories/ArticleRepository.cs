using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EkoStatApi.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }
    public IQueryable<Article> ArticlesWithAllIncludes
        => EkoStatContext.Articles
            .Include(a => a.Entries)
            .Include(a => a.Tags)
            .Include(a => a.User);

    public ArticleRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Article?> GetAsync(int id)
    {
        return await ArticlesWithAllIncludes.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetByEntryAsync(int entryId)
    {
        return await ArticlesWithAllIncludes
            .Where(a => a.Entries.Any(e => e.Id == entryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByTagAsync(int tagId)
    {
        return await ArticlesWithAllIncludes
            .Where(a => a.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByUserAsync(int userId)
    {
        return await ArticlesWithAllIncludes
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}
