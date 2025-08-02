using ToDoList.Domain.Dtos;

namespace ToDoList.Services.Contract;

public interface IUserService
{
    void RegisterUser(RegisterUserDto userDto);
    string LoginUser(LoginUserDto userDto);
}