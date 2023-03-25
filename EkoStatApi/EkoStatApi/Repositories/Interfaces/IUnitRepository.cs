using EkoStatApi.Models;

namespace EkoStatApi.Repositories.Interfaces;

internal interface IUnitRepository : IRepository<Unit>
{
    Task<Unit?> GetAsync(int id);
}
