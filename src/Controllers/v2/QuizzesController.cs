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

    [HttpGet("q1")]
    public async Task<IEnumerable<QuizV2>> GetQuizzes()
    {
        return await _context.Quizzes.Include(q => q.Questions).ToListAsync();
    }

    [HttpGet("q2")]
    public async Task<IEnumerable<QuestionV2>> GetQuestions()
    {
        return await _context.QuestionsV2.ToListAsync();
    }

    [HttpGet("quiz")]
    public async Task<IEnumerable<QuizV2>> GetQuiz(
        [FromQuery] int? id,
        [FromQuery] string? category,
        [FromQuery] string? difficulty)
    {
        var query = _context.Quizzes.Include(q => q.Questions).AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(q => q.Category.ToLower().Equals(category.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(difficulty))
        {
            query = query.Where(q => q.Difficulty.ToLower().Equals(difficulty.ToLower()));
        }
        
        if (id is not null)
        {
            return await query.Where(q => q.Id == id).ToListAsync();   
        }

        return await query.ToListAsync();
    }

    [HttpGet("question")]
    public async Task<ActionResult<QuestionV2>> GetQuestionV2([FromQuery] int id)
    {
        var quiz = await _context.QuestionsV2.FindAsync(id);

        if (quiz == null)
        {
            return NotFound();
        }

        return quiz;
    }

    [HttpPost("q1")]
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuiz(QuizV2 quizV2)
    {
        _context.Quizzes.Add(quizV2);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quizV2.Id }, quizV2);
    }

    [HttpPost("q2")]
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuestionV2(QuestionV2 question)
    {
        //question.Id = Guid.NewGuid();

        _context.QuestionsV2.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuestionV2), new { id = question.Id }, question);
    }
}