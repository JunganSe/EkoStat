using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

public interface IUnitRepository : IRepository<Unit>
{
    Task<Unit?> GetAsync(int id);
}
