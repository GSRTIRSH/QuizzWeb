using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace CMS.Models;

public class Quiz
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public string ImagePath { get; set; }

    public List<QuestionV2> Questions { get; set; }

    public string Category { get; set; }

    public string Difficulty { get; set; }

    public string Author { get; set; }
}