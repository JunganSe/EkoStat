using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetAsync(int id);
}
