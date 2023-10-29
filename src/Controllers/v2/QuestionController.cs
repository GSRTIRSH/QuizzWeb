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
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ApiAuthFilter))]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme /*, Roles = "User"*/)]
public class QuestionController : ControllerBase
{
    private QuizContextV2 _context;

    public QuestionController(QuizContextV2 context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<QuestionV2>> GetQuestions()
    {
        return await _context.QuestionsV2.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionV2>> GetQuestionV2(int id)
    {
        var quiz = await _context.QuestionsV2.FindAsync(id);
        if (quiz == null) return NotFound();
        
        return quiz;
    }

    [HttpPost]
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuestionV2(QuestionV2 question)
    {
        _context.QuestionsV2.Add(question);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetQuestionV2), new { id = question.Id }, question);
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