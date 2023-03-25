using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Data;

internal interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    IEntryRepository Entries { get; }
    ITagRepository Tags { get; }
    IUnitRepository Units { get; }
    IUserRepository Users { get; }

    Task<int> SaveAsync();
}
