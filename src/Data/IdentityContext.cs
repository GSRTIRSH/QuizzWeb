using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizzWebApi.Data;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
        Database.Migrate();
        Database.EnsureCreated();
        SaveChanges();
    }
}