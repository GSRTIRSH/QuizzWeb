using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CMS.Models;

[Table("quizzes")]
public class Quiz
{
    public int Id { get; set; }

    public string Question { get; set; }

    public string? Description { get; set; }

    public Dictionary<string, string> Answers { get; set; }

    public bool MultipleCorrectAnswers { get; set; }

    public Dictionary<string, string> CorrectAnswers { get; set; }

    public int CorrectAnswer { get; set; }

    public string? Explanation { get; set; }

    public string? Tip { get; set; }

    public List<Dictionary<string, string>> Tags { get; set; }

    public string Category { get; set; }

    public string Difficulty { get; set; }

    public int? Author { get; set; }
}