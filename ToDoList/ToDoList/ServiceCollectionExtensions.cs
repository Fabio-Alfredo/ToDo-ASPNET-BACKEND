using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;
using ToDoList.Services.Impl;

namespace ToDoList;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepositoryImpl>();
        return service;
    }

    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserServiceImpl>();
        return service;
    }
}