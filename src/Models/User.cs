using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzWebApi.Models;

[Table("users")]
public class User
{
    [Column("id")] public int Id { get; set; }

    [Column("first_name")] public string FirstName { get; set; }

    [Column("sub_name")] public string SubName { get; set; }

    [Column("email")] public string Email { get; set; }

    [Column("password_hash")] public string PasswordHash { get; set; }

    [Column("token")] public string Token { get; set; }
}