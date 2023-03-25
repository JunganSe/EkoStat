using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

internal class EntryRepository : Repository<Entry>, IEntryRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<Entry> EntriesWithIncludes
        => EkoStatContext.Entries
            .Include(e => e.Article)
            .Include(e => e.Unit);

    public EntryRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Entry?> GetAsync(int id)
    {
        return await EntriesWithIncludes
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Entry>> GetByArticleAsync(int articleId)
    {
        return await EntriesWithIncludes
            .Where(e => e.ArticleId == articleId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entry>> GetByTagAsync(int tagId)
    {
        return await EntriesWithIncludes
            .Where(e => e.Article.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Entry>> GetByUserAsync(int userId)
    {
        return await EntriesWithIncludes
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }
}
