using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Data;
using QuizzWebApi.Models;

namespace QuizzWebApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[QuizExceptionFilter]
[ServiceFilter(typeof(ApiAuthFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
public class QuestionController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly QuizContextV2 _context;

    public QuestionController(QuizContextV2 context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<QuestionV2>> GetQuestions()
    {
        return await _context.QuestionsV2.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionV2>> GetQuestion(int id)
    {
        var quiz = await _context.QuestionsV2.FindAsync(id);

        _context.QuestionsV2.Include(c => c.QuizV2Id);
        if (quiz == null) return NotFound();

        return quiz;
    }

    [HttpPost]
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuestion(QuestionV2 question)
    {
        _context.QuestionsV2.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchQuestion(int id, [FromBody] JsonElement json)
    {
        var question = await _context.QuestionsV2.FindAsync(id);
        if (question == null) return NotFound();

        var updateData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.ToString());
        if (updateData == null) return BadRequest();

        if (updateData.ContainsKey("title"))
        {
            question.Title = updateData["title"].GetString();
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.QuestionsV2.FindAsync(id);
        if (question == null) return NotFound();

        _context.QuestionsV2.Remove(question);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutQuestion(int id) => Ok();
}