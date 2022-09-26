#pragma warning disable
namespace Blog.Models;

public class CreateOrUpdateArticleViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
}