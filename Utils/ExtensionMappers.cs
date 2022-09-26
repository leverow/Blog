using Blog.Entity;
using Blog.Models;

namespace Blog.Utils;

public static class ExtensionMappers
{
    public static AppUser ToEntity(this RegisterViewModel model)
        => new AppUser()
        {
            UserName = model.Username,
            Avatar = ToBase64String(model.Avatar)
        };

    public static Article ToEntity(this CreateOrUpdateArticleViewModel model)
        => new()
        {
            Title = model.Title,
            Content = model.Content,
            Image = ToBase64String(model.Image)
        };

    public static ArticleViewModel ToModel(this Article entity)
        => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
            Image = entity.Image,
            AppUserId = entity.AppUserId,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate,
            IsEdited = entity.IsEdited
        };
    public static string ToBase64String(IFormFile image)
    {
        var memoryStream = new MemoryStream();
        image.CopyToAsync(memoryStream);
        var result = memoryStream.ToArray();
        while(result.Count() == 0) result = memoryStream.ToArray();
        var str = Convert.ToBase64String(result);
        return "data:image/jpeg;base64,"+str;
    }
}