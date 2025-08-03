using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

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

    public Token(User user, string data)
    {
        Id = Guid.NewGuid();
        User = user;
        Data = data;
        Expiration = DateTime.UtcNow.AddDays(7); 
        CanActivate = true;
    }
    
    public Token()
    {
    }
    
}