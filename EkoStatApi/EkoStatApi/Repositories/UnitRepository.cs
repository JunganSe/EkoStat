using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class UnitRepository : Repository<Unit>, IUnitRepository
{
    public EkoStatContext EkoStatContext { get => (EkoStatContext)Context; }

    public UnitRepository(EkoStatContext context)
        : base(context)
    {
    }



    public Task<Unit> GetAsync(int id)
    {
        throw new NotImplementedException();
    }
}
