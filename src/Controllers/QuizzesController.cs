using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly QuizContext _context;

    public QuizzesController(QuizContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Quiz>> GetQuizzes()
    {
        return await _context.Quizzes.ToListAsync();
    }

    [HttpGet("id:{id:int}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }

    [HttpPost]
    public async Task<ActionResult> PostQuiz(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
    }
}