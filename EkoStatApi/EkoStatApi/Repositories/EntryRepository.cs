using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class EntryRepository : Repository<Entry>, IEntryRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }
    public IQueryable<Entry> EntriesWithAllIncludes
        => EkoStatContext.Entries
            .Include(e => e.Article)
            .Include(e => e.Unit)
            .Include(e => e.User);

    public EntryRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Entry?> GetAsync(int id)
    {
        return await EntriesWithAllIncludes.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Entry>> GetByArticleAsync(int articleId)
    {
        return await EntriesWithAllIncludes
            .Where(e => e.ArticleId == articleId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entry>> GetByTagAsync(int tagId)
    {
        return await EntriesWithAllIncludes
            .Where(e => e.Article.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Entry>> GetByUserAsync(int userId)
    {
        return await EntriesWithAllIncludes
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }
}
