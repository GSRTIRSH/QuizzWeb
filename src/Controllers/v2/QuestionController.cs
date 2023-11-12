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

//TODO: add authorization
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

    /// <summary>
    /// Returns a list of all question
    /// </summary>
    /// <remarks>will be deleted in future</remarks>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<IEnumerable<QuestionV2>> GetQuestions()
    {
        return await _context.QuestionsV2.ToListAsync();
    }

    /// <summary>
    /// Find a question by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns question</response>
    /// <response code="404">Specified question doesn't exists</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    [HttpGet("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<QuestionV2>> GetQuestion(int id)
    {
        var quiz = await _context.QuestionsV2.FindAsync(id);

        _context.QuestionsV2.Include(c => c.QuizV2Id);
        if (quiz == null) return NotFound();

        return quiz;
    }

    /// <summary>
    /// Create a new question
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    /// <response code="200">Question created</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    [HttpPost]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [RequireApiKey(isAdminKey: true)]
    public async Task<ActionResult> PostQuestion(QuestionV2 question)
    {
        _context.QuestionsV2.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
    }

    /// <summary>
    /// Partly update an existing question
    /// </summary>
    /// <param name="id"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    /// <response code="200">Question updated</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified question doesn't exists</response>
    [HttpPatch("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
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

    /// <summary>
    /// Deletes a question
    /// </summary>
    /// <remarks>Currently not supported yet</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Question deleted</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified quiz doesn't exists</response>
    [HttpDelete("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.QuestionsV2.FindAsync(id);
        if (question == null) return NotFound();

        _context.QuestionsV2.Remove(question);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Update an existing question
    /// </summary>
    /// <remarks>Currently not supported yet</remarks>
    /// <param name="id"></param>
    /// <param name="question"></param>
    /// <returns></returns>
    /// <response code="200">Question updated</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified question doesn't exists</response>
    [HttpPut("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> PutQuestion(int id, [FromBody] QuestionV2 question) => Ok();
}