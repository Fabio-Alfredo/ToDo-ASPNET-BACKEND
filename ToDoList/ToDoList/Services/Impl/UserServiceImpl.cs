using ToDoList.Domain.Dtos;
using ToDoList.Domain.Entities;
using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;
using ToDoList.Utils;

namespace ToDoList.Services.Impl;

public class UserServiceImpl:IUserService
{

    private readonly IUserRepository userRepository;
    private readonly JwtTools jwtTools;
    
    public UserServiceImpl(IUserRepository userRepository, JwtTools jwtTools)
    {
        this.userRepository = userRepository;
        this.jwtTools = jwtTools;
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

    public string LoginUser(LoginUserDto userDto)
    {
        try
        {
            var user = userRepository.FindByEmail(userDto.Email);
            if( user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                throw new Exception("Invalid credentials.");
            }

            return jwtTools.GenerateToken(user);
        }catch (Exception e)
        {
            throw new Exception("An error occurred while logging in the user.", e);
        }
    }
}