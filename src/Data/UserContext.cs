using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        /*Database.Migrate();
        Database.EnsureCreated();
        SaveChanges();*/
    }

    public DbSet<User> Users { get; set; }
}