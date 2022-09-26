#pragma warning disable
namespace Blog.Models;

public class ArticleViewModel
{
    public ulong Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string AppUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsEdited { get; set; }
}