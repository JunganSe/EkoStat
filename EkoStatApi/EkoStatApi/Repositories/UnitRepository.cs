using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Repositories;

internal class UnitRepository : Repository<Unit>, IUnitRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;
    private IQueryable<Unit> UnitsWithIncludes
        => EkoStatContext.Units
            .Include(u => u.Entries);

    public UnitRepository(EkoStatContext context)
        : base(context)
    {
    }



    public async Task<Unit?> GetAsync(int id)
    {
        return await UnitsWithIncludes
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
