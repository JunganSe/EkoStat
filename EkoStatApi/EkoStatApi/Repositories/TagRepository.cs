using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

internal class TagRepository : Repository<Tag>, ITagRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<Tag> TagsWithIncludes
        => EkoStatContext.Tags
            .Include(t => t.Articles);

    public TagRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Tag?> GetAsync(int id)
    {
        return await TagsWithIncludes
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tag>> GetByArticleAsync(int articleId)
    {
        return await EkoStatContext.Tags
            .Where(t => t.Articles.Any(a => a.Id == articleId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Tag>> GetByUserAsync(int userId)
    {
        return await TagsWithIncludes
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}
