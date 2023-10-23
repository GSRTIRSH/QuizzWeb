using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[QuizExceptionFilter]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ApiAuthFilter))]
public class QuizzesController : ControllerBase
{
    private readonly QuizContext _context;

    public QuizzesController(QuizContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<QuestionV1>> GetQuizzes(
        [FromQuery] int limit = 1,
        [FromQuery] string? category = "",
        /*[FromQuery] string tags = "",*/
        [FromQuery] string? difficulty = "")
    {
        var query = _context.Quizzes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(q => q.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(difficulty))
        {
            query = query.Where(q => q.Difficulty.Equals(difficulty, StringComparison.CurrentCultureIgnoreCase));
        }

        var result = await query.Take(limit).ToListAsync();

        return result;
    }

    [HttpGet("id:{id:int}")]
    public async Task<ActionResult<QuestionV1>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }

    [HttpPost]
    public async Task<ActionResult> PostQuiz(QuestionV1 question)
    {
        _context.Quizzes.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = question.Id }, question);
    }
}