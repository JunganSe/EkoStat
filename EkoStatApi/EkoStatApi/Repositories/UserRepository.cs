using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }
    public IQueryable<User> UsersWithAllIncludes
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
        return await UsersWithAllIncludes.FirstOrDefaultAsync(u => u.Id == id);
    }
}
