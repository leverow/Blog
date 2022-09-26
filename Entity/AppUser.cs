#pragma warning disable
using Microsoft.AspNetCore.Identity;

namespace Blog.Entity;

public class AppUser : IdentityUser
{
    public string Avatar { get; set; }
    public virtual ICollection<Article> Articles { get; set; }
}