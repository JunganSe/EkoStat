using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

internal interface IUserRepository : IRepository<User>
{
    Task<User?> GetAsync(int id);
}
