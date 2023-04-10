using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Data;

public interface IUnitOfWork : IDisposable
{
    public IArticleRepository Articles { get; }
    public IEntryRepository Entries { get; }
    public ITagRepository Tags { get; }
    public IUnitRepository Units { get; }
    public IUserRepository Users { get; }

    Task<bool> TrySaveAsync();
}
