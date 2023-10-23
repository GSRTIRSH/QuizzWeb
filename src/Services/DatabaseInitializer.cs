using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Data;

namespace QuizzWebApi.Services;

public class DatabaseInitializer
{
    private readonly QuizContext _context;

    public DatabaseInitializer(QuizContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        Console.WriteLine("FUCK YOU");
        Console.WriteLine("FUCK YOU");
        Console.WriteLine("FUCK YOU");
        Console.WriteLine("FUCK YOU");
        Console.WriteLine("FUCK YOU");
        Console.WriteLine("FUCK YOU");

        _context.Database.Migrate();
        _context.Database.EnsureDeleted();
    }
}