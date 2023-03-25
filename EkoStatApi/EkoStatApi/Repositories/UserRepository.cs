using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

internal class UserRepository : Repository<User>, IUserRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<User> UsersWithIncludes
        => EkoStatContext.Users
            .Include(u => u.Tags)
            .Include(u => u.Articles)
            .Include(u => u.Entries);

    public UserRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<User?> GetAsync(int id)
    {
        return await UsersWithIncludes
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
