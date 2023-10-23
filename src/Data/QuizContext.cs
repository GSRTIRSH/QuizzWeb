using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class QuizContext : DbContext
{
    public QuizContext(DbContextOptions<QuizContext> options) : base(options)
    {
        Database.Migrate();
        Database.EnsureCreated();
        SaveChanges();
    }

    public DbSet<QuestionV1> Quizzes { get; set; }
}