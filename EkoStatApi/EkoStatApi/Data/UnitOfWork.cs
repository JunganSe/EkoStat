using EkoStatApi.Repositories;
using EkoStatApi.Repositories.Interfaces;

namespace EkoStatApi.Data;

internal class UnitOfWork : IUnitOfWork
{
    private readonly EkoStatContext _ekoStatContext;
    private IArticleRepository? _articleRepository;
    private IEntryRepository? _entryRepository;
    private ITagRepository? _tagRepository;
    private IUnitRepository? _unitRepository;
    private IUserRepository? _userRepository;

    public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_ekoStatContext);
    public IEntryRepository Entries => _entryRepository ??= new EntryRepository(_ekoStatContext);
    public ITagRepository Tags => _tagRepository ??= new TagRepository(_ekoStatContext);
    public IUnitRepository Units => _unitRepository ??= new UnitRepository(_ekoStatContext);
    public IUserRepository Users => _userRepository ??= new UserRepository(_ekoStatContext);

    public UnitOfWork(EkoStatContext ekoStatContext)
    {
        _ekoStatContext = ekoStatContext;
    }

    public async Task<int> SaveAsync()
    {
        return await _ekoStatContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _ekoStatContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
