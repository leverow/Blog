using Blog.Data;
using Blog.Entity;

namespace Blog.Repositories;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext context)
        : base(context) { }
}