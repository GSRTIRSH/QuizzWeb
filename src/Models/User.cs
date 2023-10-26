using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QuizzWebApi.Models;

[Table("users")]
public class User : IdentityUser
{
    [Column("my_quizzes")]
    public int[]? MyQuizzes { get; set; }
}
