#pragma warning disable
namespace Blog.Models;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
}