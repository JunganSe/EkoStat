using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class EntryRepository : Repository<Entry>, IEntryRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<Entry> Entries
        => EkoStatContext.Entries
            .Include(e => e.Article)
                .ThenInclude(a => a.Tags)
            .Include(e => e.Unit);

    public EntryRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Entry?> GetAsync(int id)
    {
        return await Entries
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ICollection<Entry>> GetByArticleAsync(int articleId)
    {
        return await Entries
            .Where(e => e.ArticleId == articleId)
            .ToListAsync();
    }

    public async Task<ICollection<Entry>> GetByTagAsync(int tagId)
    {
        return await Entries
            .Where(e => e.Article.Tags.Any(t => t.Id == tagId))
            .ToListAsync();
    }

    public async Task<ICollection<Entry>> GetByUserAsync(int userId)
    {
        return await Entries
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }
}
