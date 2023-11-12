using Microsoft.AspNetCore.Mvc;
using RoomChatApp.FormModels;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult Login([FromBody] UserForm userForm)
    {
        try
        {
            var user = _userService.TryLogin(userForm.Username, userForm.Password);
            return Ok(user.Id);
        }
        catch(UnauthorizedAccessException)  
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    [Route("register")]
    public ActionResult Register([FromBody] UserForm userForm)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = userForm.Username,
            Password = userForm.Password
        };

        _userService.CreateUser(user);
        return Ok();
    }

    [HttpGet]
    [Route("user-exists/{userId}")]
    public ActionResult UserExists(string userId)
    {
        return Ok(_userService.UserExists(userId));
    }

    
}