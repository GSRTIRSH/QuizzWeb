using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class QuizContext : DbContext
{
    public QuizContext(DbContextOptions<QuizContext> options) : base(options)
    {
        //create table if not exist;
    }

    public DbSet<Quiz> Quizzes { get; set; }
}