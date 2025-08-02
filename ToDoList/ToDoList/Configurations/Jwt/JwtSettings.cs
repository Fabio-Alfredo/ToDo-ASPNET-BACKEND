namespace ToDoList.Configurations;

public class JwtSettings
{
    public string SecretKey { get; set; } 
    public int ExpirationMinutes { get; set; }
}