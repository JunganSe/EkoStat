using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<Article> Articles
        => EkoStatContext.Articles
            .Include(a => a.Tags);

    public ArticleRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Article?> GetAsync(int id)
    {
        return await Articles
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ICollection<Article>> GetByEntryAsync(int entryId)
    {
        return await Articles
            .Include(a => a.Entries)
                .ThenInclude(e => e.Unit)
            .Where(a => a.Entries.Any(e => e.Id == entryId))
            .ToListAsync();
    }

    public async Task<ICollection<Article>> GetByTagAsync(int tagId)
    {
        return await Articles
            .Where(a => a.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<ICollection<Article>> GetByUserAsync(int userId)
    {
        return await Articles
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}
