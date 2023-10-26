using System.ComponentModel.DataAnnotations;

namespace QuizzWebApi.Models.Identity;

public class UserLoginDto
{
    [Required]
    public string Name { get; set; }

    [Required] 
    public string Password { get; set; }
}