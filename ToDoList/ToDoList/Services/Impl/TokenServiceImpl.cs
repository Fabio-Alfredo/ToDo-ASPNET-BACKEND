using ToDoList.Domain.Entities;
using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;
using ToDoList.Utils;

namespace ToDoList.Services.Impl;

public class TokenServiceImpl:ITokenService
{

    private readonly ITokenRepository tokenRepository;
    private readonly JwtTools jwtTools;

    public TokenServiceImpl(ITokenRepository tokenRepository, JwtTools jwtTools)
    {
        this.tokenRepository = tokenRepository;
        this.jwtTools = jwtTools;
    }
    
    public void CleanToken(User user)
    {
        var tokens = tokenRepository.FindByUserAndCanActivate(user.Id, true);

        foreach (var token in tokens.Result)
        {
            if (!jwtTools.VerifyToken(token.Data))
            {
                token.CanActivate = false;
                tokenRepository.Save(token).Wait();
            }
        }
    }

    public async Task<Token> RegisterToken(User user)
    {
        CleanToken(user);
        var tokenString =jwtTools.GenerateToken(user);
        var newToken = new Token(user, tokenString);
        
        return await tokenRepository.Save(newToken);
    }

    public async Task<bool> IsValidToken(User user, string token)
    {
        CleanToken(user);
        var tokens = await tokenRepository.FindByUserAndCanActivate(user.Id, true);
        
        var exists = tokens.Any(t=> t.Data == token);
        
        return exists;
    }
}