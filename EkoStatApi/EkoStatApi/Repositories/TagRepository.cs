using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public EkoStatContext EkoStatContext => (EkoStatContext)Context;
    public IQueryable<Tag> TagsWithAllIncludes
        => EkoStatContext.Tags
            .Include(t => t.Articles)
            .Include(t => t.User);

    public TagRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Tag?> GetAsync(int id)
    {
        return await TagsWithAllIncludes
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
        return await TagsWithAllIncludes
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}
