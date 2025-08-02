using ToDoList.Domain.Dtos;
using ToDoList.Domain.Entities;
using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;

namespace ToDoList.Services.Impl;

public class UserServiceImpl:IUserService
{

    private readonly IUserRepository userRepository;
    
    public UserServiceImpl(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public void RegisterUser(RegisterUserDto userDto)
    {
        try
        {
            var user = userRepository.FindByEmail(userDto.Email);
            if (user != null)
            {
                throw new Exception("User with this email already exists.");
            }
            user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };
            
            userRepository.Save(user);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while registering the user.", e);
        }
    }
}