using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace QuizzWebApi.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")] 
    public int Id { get; set; }

    [Required]
    [Column("first_name")] 
    public string FirstName { get; set; }
    
    [Column("sub_name")] 
    public string SubName { get; set; }

    [Required]
    [Column("email")] 
    public string Email { get; set; }

    [Required]
    [Column("password_hash")] 
    public string PasswordHash { get; set; }

    [Column("token")] 
    public string Token { get; set; }
    
    [Column("my_quizzes")]
    public int[]? MyQuizzes { get; set; }
}
