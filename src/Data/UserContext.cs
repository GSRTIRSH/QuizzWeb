using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
}