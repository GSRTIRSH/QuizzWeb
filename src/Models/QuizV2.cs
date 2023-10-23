using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzWebApi.Models;

[Table("quizzes_v2")]
public class QuizV2
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    [Column("image_path")]
    public string ImagePath { get; set; }

    [Required]
    public List<QuestionV2> Questions { get; set; }
    
    /*[Required]
    public List<Tag> Tags { get; set; }*/
    
    [Required]
    public string Category { get; set; }

    [Required]
    public string Difficulty { get; set; }
    
    [Required]
    public int Author { get; set; }
    
    /*[Required]
    [Column("quiz_id")]
    public string QuizId { get; set; }*/
}