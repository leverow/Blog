using Blog.Data;
using Blog.Entity;

namespace Blog.Repositories;

public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        : base(context) { }
}