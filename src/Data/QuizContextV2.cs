using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;

namespace QuizzWebApi.Data;

public class QuizContextV2 : DbContext
{
    public QuizContextV2(DbContextOptions<QuizContextV2> options) : base(options)
    {
        //create table if not exist;
    }

    public DbSet<QuizV2> Quizzes { get; set; }
}