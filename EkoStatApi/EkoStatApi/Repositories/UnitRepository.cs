﻿using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Repositories;

public class UnitRepository : Repository<Unit>, IUnitRepository
{
    private EkoStatContext EkoStatContext => (EkoStatContext)Context;

    public UnitRepository(EkoStatContext context)
        : base(context)
    {
    }
}
