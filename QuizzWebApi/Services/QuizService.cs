using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Data;
using QuizzWebApi.Models;
using QuizzWebApi.Models.Entities;
using QuizzWebApi.Services.Interfaces;

namespace QuizzWebApi.Services;

public class QuizService : IQuizService
{
    private readonly IMapper _mapper;
    private readonly QuizContextV2 _context;
    private readonly UserManager<User> _userManager;

    public QuizService(IMapper mapper, QuizContextV2 context, UserManager<User> userManager)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
    }

    public async Task<PagedList<QuizV2>> GetQuizzes(int pageNumber, int pageSize, string? category,
        string? difficulty)
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

        return await PagedList<QuizV2>.ToPagedListAsync(query, pageNumber, pageSize);
    }

    public async Task<QuizV2?> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes
            .Where(q => q.Id == id)
            .Include(q => q.Questions)
            .SingleOrDefaultAsync();
        return quiz;
    }

    public async Task PostQuiz(QuizV2 quizV2)
    {
        _context.Quizzes.Add(quizV2);
        await _context.SaveChangesAsync();
    }

    public async Task PatchQuiz(int id, Dictionary<string, JsonElement> updateData)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return;

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
    }

    public async Task DeleteQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return;

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task PutQuiz(int id, QuizV2 quizV2)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
    }
}