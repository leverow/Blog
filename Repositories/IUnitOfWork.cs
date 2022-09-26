namespace Blog.Repositories;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    int Save();
}