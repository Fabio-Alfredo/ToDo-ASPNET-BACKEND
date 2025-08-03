using System.Text.Json;
using ToDoList.Utils.Exceptions;

namespace ToDoList.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "An unexpected error occurred.";

        if (ex is HttpException httpException)
        {
            statusCode = httpException.StatusCode;
            message = httpException.Message;
        }

        var response = new
        {
            statusCode,
            message
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        
    }
        
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error no controlado");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    
}