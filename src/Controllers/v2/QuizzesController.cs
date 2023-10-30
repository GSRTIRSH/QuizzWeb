using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[QuizExceptionFilter]
[ServiceFilter(typeof(ApiAuthFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
public class QuizzesController : ControllerBase
{
    private readonly QuizContextV2 _context;
    private readonly UserManager<IdentityUser> _userManager;

    public QuizzesController(QuizContextV2 context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
    [TypeFilter(typeof(JwtTokenFilter))]
    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes
            .Where(q => q.Id == id)
            .Include(q => q.Questions)
            .SingleOrDefaultAsync();

        if (quiz == null) return NotFound();

        if (!ValidateToken(quiz, out var result)) return result;

        return quiz;
    }

    [HttpPost]
    [TypeFilter(typeof(JwtTokenFilter))]
    public async Task<ActionResult> PostQuiz(QuizV2 quizV2)
    {
        if (!ValidateToken(quizV2, out var result)) return result;

        _context.Quizzes.Add(quizV2);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quizV2.Id }, quizV2);
    }

    [HttpPatch("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] JsonElement json)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        var updateData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.ToString());
        if (updateData == null) return BadRequest();

        if (!ValidateToken(quiz, out var result)) return result;

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
    [TypeFilter(typeof(JwtTokenFilter))]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] QuizV2 quizV2) => Ok();

    private bool ValidateToken(QuizV2 quiz, out ActionResult result)
    {
        result = Ok();

        if (!HttpContext.Items.TryGetValue("JsonToken", out var jsonToken))
        {
            result = Unauthorized();
            return false;
        }

        if (jsonToken is not JwtSecurityToken token)
        {
            result = Unauthorized();
            return false;
        }

        var id = token.Claims.First(claim => claim.Type == "id").Value;
        var role = token.Claims.First(claim => claim.Type == "role").Value;

        if (role == "Admin") return true;

        var user = _userManager.FindByIdAsync(id).Result;
        if (user.Id == quiz.Author) return true;

        result = Forbid();
        return false;
    }
}