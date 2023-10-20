using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace QuizzWebApi.Models;

[Table("quizzesv1")]
public class QuizV1
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("question")]
    public string Question { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }

    [Column("answers", TypeName = "jsonb")]
    public Dictionary<string, string> Answers { get; set; }

    [Column("multiple_correct_answers")] 
    public bool MultipleCorrectAnswers { get; set; }

    [Column("correct_answers", TypeName = "jsonb")]
    public Dictionary<string, string> CorrectAnswers { get; set; }
    
    [Column("correct_answer")] 
    public int CorrectAnswer { get; set; }

    [Column("explanation")] 
    public string? Explanation { get; set; }
    
    [Column("tip")] 
    public string? Tip { get; set; }
    
    [Required]
    [Column("tags", TypeName = "jsonb")] 
    public List<Dictionary<string, string>> Tags { get; set; }
    
    [Required]
    [Column("category")] 
    public string Category { get; set; }

    [Required]
    [Column("difficulty")] 
    public string Difficulty { get; set; }
}

[Table("quizzesV2")]
public class QuizV2
{
    [Key] 
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("question")]
    public string Question { get; set; }
    
    [Required]
    [Column("image_path")]
    public string ImagePath { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [Column("answers", TypeName = "jsonb")]
    public Dictionary<char, string> Answers { get; set; }
    
    [Required]
    [Column("correct_answers", TypeName = "jsonb")]
    public Dictionary<char, string> CorrectAnswers { get; set; }
    
    [Column("explanation")] 
    public string? Explanation { get; set; }
    
    [Column("tip")] 
    public string? Tip { get; set; }
    
    [Required]
    [Column("tags")] 
    public int[] Tags { get; set; }
    
    [Required]
    [Column("category")] 
    public string Category { get; set; }

    [Required]
    [Column("difficulty")] 
    public string Difficulty { get; set; }
    
    [Required]
    [Column("author")] 
    public int? Author { get; set; }
}