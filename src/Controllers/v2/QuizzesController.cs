using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme /*, Roles = "User"*/)]
public class QuizzesController : ControllerBase
{
    private readonly QuizContextV2 _context;

    public QuizzesController(QuizContextV2 context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<QuizV2>> GetQuizzes(
        [FromQuery] int? limit,
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

        if (limit != null)
        {
            return await query.Take((int)limit).ToListAsync();
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();

        if (quiz == null) return NotFound();

        return quiz;
    }

    [HttpPost]
    public async Task<ActionResult> PostQuiz(QuizV2 quizV2)
    {
        _context.Quizzes.Add(quizV2);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quizV2.Id }, quizV2);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] JsonElement json)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        var updateData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.ToString());
        if (updateData == null) return BadRequest();

        if (updateData.ContainsKey("title"))
        {
            quiz.Title = updateData["title"].GetString();
        }

        if (updateData.ContainsKey("imagePath"))
        {
            quiz.ImagePath = updateData["imagePath"].GetString();
        }

        if (updateData.ContainsKey("category"))
        {
            quiz.Category = updateData["category"].GetString();
        }

        if (updateData.ContainsKey("difficulty"))
        {
            quiz.Difficulty = updateData["difficulty"].GetString();
        }

        if (updateData.ContainsKey("author"))
        {
            quiz.Author = updateData["author"].GetString();
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [RequireApiKey(isAdminKey: true)]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    [RequireApiKey(isAdminKey: true)]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] QuizV2 quizV2) => Ok();
}