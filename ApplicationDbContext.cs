using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB;

public class ApplicationDbContext : IdentityDbContext<UserModel, RoleModel, string>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

}