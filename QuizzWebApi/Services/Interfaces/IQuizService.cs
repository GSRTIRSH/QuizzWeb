using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Models;
using QuizzWebApi.Models.Entities;

namespace QuizzWebApi.Services.Interfaces;

public interface IQuizService
{
    public Task<PagedList<QuizV2>> GetQuizzes(int pageNumber, int pageSize, string? category, string? difficulty);

    public Task<QuizV2?> GetQuiz(int id);

    public Task PostQuiz(QuizV2 quizV2);

    public Task PatchQuiz(int id, Dictionary<string, JsonElement> json);

    public Task DeleteQuiz(int id);

    public Task PutQuiz(int id, QuizV2 quizV2);
}