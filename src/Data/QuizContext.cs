using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class QuizContext : DbContext
{
    public QuizContext(DbContextOptions<QuizContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
        SaveChanges();
    }

    public DbSet<QuizV1> Quizzes { get; set; }
}