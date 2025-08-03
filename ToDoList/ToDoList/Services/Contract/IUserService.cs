using ToDoList.Domain.Dtos;
using ToDoList.Domain.Entities;

namespace ToDoList.Services.Contract;

public interface IUserService
{
    Task RegisterUser(RegisterUserDto userDto);
    Task<Token> LoginUser(LoginUserDto userDto);
    Task<User> GetUserByEmail(string email);
}