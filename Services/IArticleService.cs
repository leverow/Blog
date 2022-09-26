using Blog.Models;

namespace Blog.Services;

public interface IArticleService
{
    ValueTask<Result<ArticleViewModel>> CreateArticleAsync(CreateOrUpdateArticleViewModel model, string userId);
    ValueTask<Result<ArticleViewModel>> UpdateArticleAsync(ulong id, CreateOrUpdateArticleViewModel model);
    ValueTask<Result<ArticleViewModel>> GetArticleAsync(ulong id);
    ValueTask<Result<IEnumerable<ArticleViewModel>>> GetArticlesAsync(int page = 1, int limit = 10, string title = "", string authorName = "");
    ValueTask<Result<ArticleViewModel>> DeleteArticleAsync(ulong id);
}