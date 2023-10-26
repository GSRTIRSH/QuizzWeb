using System.ComponentModel.DataAnnotations;

namespace QuizzWebApi.Models.Identity;

public class UserRegistrationDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }
    
    /*[Required] 
    public string ConfirmPassword { get; set; }*/
}