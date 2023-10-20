namespace QuizzWebApi.Models;

/*[Table("tags")]*/
public class Tag
{
    /*[Key]
    [Column("id")]
    public int Id { get; set; }*/

    /*[Column("name")]*/
    public string Name { get; set; }

    /*[Column("color")]*/
    public string Color { get; set; }
}