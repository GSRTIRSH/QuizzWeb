using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzWebApi.Models;

[Table("questions_v2")]
public class QuestionV2
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Column("answers", TypeName = "jsonb")] 
    public Dictionary<char, string> Answers { get; set; }
    
    [Required]
    [Column("correct_answers", TypeName = "jsonb")] 
    public Dictionary<char, string> CorrectAnswers { get; set; }
    
    [Required]
    public QuizV2 QuizV2 { get; set; }
}