using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)_context;
    private IQueryable<Tag> Tags
        => EkoStatContext.Tags;

    public TagRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<ICollection<Tag>> GetByArticleAsync(int articleId)
    {
        return await EkoStatContext.Tags
            .Include(t => t.Articles)
            .Where(t => t.Articles.Any(a => a.Id == articleId))
            .ToListAsync();
    }

    public async Task<ICollection<Tag>> GetByUserAsync(int userId)
    {
        return await Tags
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<ICollection<Tag>> GetByIdsAsync(List<int> tagIds)
    {
        return await Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync();
    }
}
