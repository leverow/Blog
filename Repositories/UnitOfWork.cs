using Blog.Data;

namespace Blog.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IArticleRepository Articles { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Articles = new ArticleRepository(context);
    }
    
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Save()
        => _context.SaveChanges();
}