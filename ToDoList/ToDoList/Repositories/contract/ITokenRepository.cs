using ToDoList.Domain.Entities;

namespace ToDoList.Repositories.impl;

public interface ITokenRepository
{
    Task<Token> Save(Token token);
    Task<Token?> FindById(Guid id);
    Task<List<Token>>FindByUserAndCanActivate(Guid userId, bool canActivate);
}