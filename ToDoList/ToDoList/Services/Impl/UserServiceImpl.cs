using ToDoList.Domain.Dtos;
using ToDoList.Domain.Entities;
using ToDoList.Repositories.impl;
using ToDoList.Services.Contract;
using ToDoList.Utils;

namespace ToDoList.Services.Impl;

public class UserServiceImpl:IUserService
{

    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;
    
    public UserServiceImpl(IUserRepository userRepository, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
    }
    
    public async Task RegisterUser(RegisterUserDto userDto)
    {
        try
        {
            var user = await userRepository.FindByEmail(userDto.Email);
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
            
            await userRepository.Save(user);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while registering the user.", e);
        }
    }

    public async Task<Token> LoginUser(LoginUserDto userDto)
    {
        try
        {
            
            var user = await userRepository.FindByEmail(userDto.Email);
            
            if( user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                throw new Exception("Invalid credentials.");
            }
           
            var token = await tokenService.RegisterToken(user);
            return token;
        }catch (Exception e)
        {
            throw new Exception("An error occurred while logging in the user.", e);
        }
    }
}