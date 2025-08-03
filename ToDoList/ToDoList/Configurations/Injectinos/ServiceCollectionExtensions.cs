using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;
using ToDoList.Services.Impl;
using ToDoList.Utils;

namespace ToDoList;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepositoryImpl>();
        service.AddScoped<ITokenRepository, TokenRepositoryImpl>();
        return service;
    }

    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserServiceImpl>();
        service.AddScoped<ITokenService, TokenServiceImpl>();
        return service;
    }
    
    public static IServiceCollection AddUtils(this IServiceCollection service)
    {
        service.AddScoped<JwtTools>();
        return service;
    }
}