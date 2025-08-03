using ToDoList.Services.Contract;
using ToDoList.Utils;

namespace ToDoList.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate next;

    public AuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITokenService tokenService, IUserService userService, JwtTools jwtTools)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var email = jwtTools.GetEmailFromToken(token);
                if (!string.IsNullOrEmpty(email))
                {
                    var user = await userService.GetUserByEmail(email);
                    var isValid = await tokenService.IsValidToken(user, token);
                    if (!isValid)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token revocado o inválido.");
                        return;
                    }
                }
                
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido o corrupto.");
                return;
            }
        }
        await next(context);
    }
}