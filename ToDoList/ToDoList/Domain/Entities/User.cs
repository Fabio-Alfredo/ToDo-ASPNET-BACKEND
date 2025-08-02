using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
    
    public ICollection<Token> Tokens { get; set; } = new List<Token>();
}