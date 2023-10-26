using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v1;

[ApiController]
[QuizExceptionFilter]
[ApiVersion("1.0", Deprecated = true)]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ApiAuthFilter))]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            query = query.Where(q => q.Category.ToLower().Equals(category.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(difficulty))
        {
            query = query.Where(q => q.Difficulty.ToLower().Equals(difficulty.ToLower()));
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
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuestion(QuestionV1 question)
    {
        _context.Quizzes.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = question.Id }, question);
    }
}