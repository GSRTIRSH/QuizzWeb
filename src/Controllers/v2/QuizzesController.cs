using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[QuizExceptionFilter]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ApiAuthFilter))]
public class QuizzesController : ControllerBase
{
    private readonly QuizContextV2 _context;

    public QuizzesController(QuizContextV2 context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<QuizV2>> GetQuizzes()
    {
        return await _context.Quizzes.ToListAsync();
    }

    [HttpGet("id:{id:int}")]
    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }
    
    [HttpGet()]
    /*public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }

    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }*/
    
    [HttpPost]
    public async Task<ActionResult> PostQuiz(QuizV2 quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
    }
}