using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Dtos;
using ToDoList.Services.Contract;
using ToDoList.Utils.Exceptions;

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
    public async Task<IActionResult>  RegisterUser([FromBody]RegisterUserDto userDto)
    {

        try
        {
            await userService.RegisterUser(userDto);
            return GeneralResponse.GetResponse(StatusCodes.Status201Created, "User registered successfully.");
        }
        catch (HttpException e)
        {
            return GeneralResponse.GetResponse(e.StatusCode, e.Message);
        }
       
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody]LoginUserDto userDto)
    {
        try
        {
            var token = await userService.LoginUser(userDto);
            return GeneralResponse.GetResponse(StatusCodes.Status200OK, "Login successful.", token.Data);
        }
        catch (HttpException e)
        {
            return GeneralResponse.GetResponse(e.StatusCode, e.Message);
        }
    }
}