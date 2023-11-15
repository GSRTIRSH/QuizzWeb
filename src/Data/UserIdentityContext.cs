using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class UserIdentityContext : IdentityDbContext<User>
{
    public UserIdentityContext(DbContextOptions<UserIdentityContext> options) : base(options)
    {
        Database.Migrate();
        Database.EnsureCreated();
        SaveChanges();
    }
}