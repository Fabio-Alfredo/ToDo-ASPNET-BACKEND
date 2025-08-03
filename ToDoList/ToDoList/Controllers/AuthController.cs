using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async  Task<ActionResult> RegisterUser([FromBody]RegisterUserDto userDto)
    {
        try
        {
             await userService.RegisterUser(userDto);
            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> LoginUser([FromBody]LoginUserDto userDto)
    {
        try
        {
            var token = await userService.LoginUser(userDto);
            return Ok(new { Message = "User logged in successfully.", Token = token.Data });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
    }
}