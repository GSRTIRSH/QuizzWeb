using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class QuizContextV2 : DbContext
{
    public QuizContextV2(DbContextOptions<QuizContextV2> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuizV2>()
            .HasMany(a => a.Questions)
            .WithOne(b => b.QuizV2)
            .HasForeignKey(b => b.Id);
    }

    public DbSet<QuizV2> Quizzes { get; set; }
    public DbSet<QuestionV2> QuestionsV2 { get; set; }
}