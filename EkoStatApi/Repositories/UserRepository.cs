using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<User> UsersWithIncludes
        => EkoStatContext.Users;

    public UserRepository(EkoStatContext context)
        : base(context)
    {
    }
}
