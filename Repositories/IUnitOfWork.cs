namespace Blog.Repositories;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    IUserRepository Users { get; }
    int Save();
}