using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dtos;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}