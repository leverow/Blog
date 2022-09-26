using System.Linq.Expressions;
using System.Security.Cryptography;
using Blog.Entity;
using Blog.Models;
using Blog.Repositories;
using Blog.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class ArticleService : IArticleService
{
    private readonly ILogger<ArticleService> _logger;
    private readonly IArticleRepository _articleRepository;
    private readonly UserManager<AppUser> _userManager;

    public ArticleService(
        ILogger<ArticleService> logger,
        IArticleRepository articleRepository,
        UserManager<AppUser> userManager
    )
    {
        _logger = logger;
        _articleRepository = articleRepository;
        _userManager = userManager;
    }

    public async ValueTask<Result<ArticleViewModel>> CreateArticleAsync(CreateOrUpdateArticleViewModel model, string userId)
    {
        try
        {
            var entity = await _articleRepository.AddAsync(model.ToEntity());
            entity.AppUserId = userId;
            return new(true) { Data = entity.ToModel() };
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured on creating article in {nameof(CreateArticleAsync)}");
            throw new("Couldn't create article. Please contact support!",e);
        }
    }

    public async ValueTask<Result<ArticleViewModel>> DeleteArticleAsync(ulong id)
    {
        var entity = _articleRepository.GetById(id);
        if(entity is null)
            return new("Article with given Id Not Found.");
        try
        {
            await _articleRepository.Remove(entity);
            return new(true);
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured on deleting article in {nameof(ArticleService)}", e);
            throw new("Coudn't remove article. Please contact support", e);
        }
    }

    public async ValueTask<Result<ArticleViewModel>> GetArticleAsync(ulong id)
    {
        try
        {
            var entity = await _articleRepository.GetAll().FirstOrDefaultAsync(a => a.Id == id);
            if(entity is null)
                return new("Article with given Id Not Found.");
            
            return new(true) { Data = entity.ToModel() };
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(ArticleService)}", e);
            throw new("Couldn't get article. Contact support.", e);
        }
    }

    public async ValueTask<Result<IEnumerable<ArticleViewModel>>> GetArticlesAsync(int page = 1, int limit = 10, string title = "", string authorName = "")
    {
        try
        {
            var filter = ArticleFilter(title.ToLower() ?? string.Empty, authorName?.ToLower() ?? string.Empty);

            var existingArticles = _articleRepository.GetAll()
                .Where(filter)
                .Skip((page - 1) * limit)
                .Take(limit);

            if (existingArticles is null)
                return new("No articles found. contact support.");

            var articles = await existingArticles.Select(q => q.ToModel()).ToListAsync();

            return new(true) { Data = articles };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured at {nameof(ArticleService)}", e);
            throw new("Couldn't get articles. Contact support.", e);
        }
    }

    public async ValueTask<Result<ArticleViewModel>> UpdateArticleAsync(ulong id, CreateOrUpdateArticleViewModel model)
    {
        var existingArticle = _articleRepository.GetById(id);

        if (existingArticle is null)
            return new("Article with given ID not found.");

        try
        {
            var updatedArticle = await _articleRepository.Update(existingArticle);

            return new(true) { Data = updatedArticle.ToModel() };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured at {nameof(ArticleService)}", e);
            throw new("Couldn't update article. Contact support.", e);
        }
    }

    private static Expression<Func<Entity.Article, bool>> ArticleFilter(string title, string authorName)
    {
        Expression<Func<Entity.Article, bool>> filter = article
        => article.Title!.Contains(title)
        || article.Author.UserName.Contains(authorName);

        return filter;
    }
}