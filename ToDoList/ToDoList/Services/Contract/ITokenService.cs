using ToDoList.Domain.Entities;

namespace ToDoList.Services.Contract;

public interface ITokenService
{
    void CleanToken(User user);
    Task<Token> RegisterToken(User user);
    Task<bool> IsValidToken(User user, string token);
}