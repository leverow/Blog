#pragma warning disable

namespace Blog.Models;

public class RegisterViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public IFormFile Avatar { get; set; }
    public string ReturnUrl { get; set; }
}