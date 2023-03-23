using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }

    public UserRepository(EkoStatContext context)
        : base(context)
    {
    }



    public Task<User> GetAsync(int id)
    {
        throw new NotImplementedException();
    }
}
