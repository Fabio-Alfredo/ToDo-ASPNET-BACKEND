using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Dtos;
using ToDoList.Services.Contract;

namespace ToDoList.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly IUserService userService;
    
    public AuthController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("register")]
    public  ActionResult RegisterUser([FromBody]RegisterUserDto userDto)
    {
        try
        {
            userService.RegisterUser(userDto);
            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while registering the user.", e);
        }
    }
}