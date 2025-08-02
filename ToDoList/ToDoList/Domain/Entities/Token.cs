using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Entities;

[Table("tokens")]
public class Token
{
    public Guid Id { get; set; }
    public string Data { get; set; }
    public DateTime Expiration { get; set; }
    public bool CanActivate { get; set; }
    
    [ForeignKey("user_id")]
    public User User { get; set; }
    
    
}