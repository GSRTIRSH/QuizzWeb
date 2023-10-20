using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]

public class QuizzesController : ControllerBase
{
    private readonly QuizContext _context;

    public QuizzesController(QuizContext context)
    {
        _context = context;
    }

    [HttpGet]
    [QuizExceptionFilter]
    public async Task<IEnumerable<QuizV1>> GetQuizzes()
    {
        return await _context.Quizzes.ToListAsync();
    }

    [HttpGet("id:{id:int}")]
    public async Task<ActionResult<QuizV1>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }

    [HttpPost]
    public async Task<ActionResult> PostQuiz(QuizV1 quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
    }
}