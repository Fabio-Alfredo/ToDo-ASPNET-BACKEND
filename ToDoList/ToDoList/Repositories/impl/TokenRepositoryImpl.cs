using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Domain.Entities;

namespace ToDoList.Repositories.impl;

public class TokenRepositoryImpl :ITokenRepository
{

    private readonly AppDbContext appDbContext;
    
    public TokenRepositoryImpl (AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
    public async Task<Token>  Save(Token token)
    {
        appDbContext.Tokens.Add(token);
         await appDbContext.SaveChangesAsync();
         return token;
    }

    public async Task<Token?> FindById(Guid id)
    {
        var token = await appDbContext.Tokens.FindAsync(id);
        return token ?? null;
    }

    public Task<List<Token>> FindByUserAndCanActivate(Guid userId, bool canActivate)
    {
        var tokens = appDbContext.Tokens
            .Where(u => u.User.Id == userId && u.CanActivate == canActivate)
            .ToListAsync();
        return tokens;
    }
}