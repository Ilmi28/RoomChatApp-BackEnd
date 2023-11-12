using Microsoft.AspNetCore.Mvc;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoomChatApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;

    public ChatsController(IChatService chatService, IUserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }

    [HttpGet]
    [Route("{chatId}")]
    public ActionResult Get(string chatId)
    {
        try
        {
            if (!_chatService.ChatExists(chatId)) return NotFound();
            var chat = _chatService.GetChat(chatId);
            return Ok(chat);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    // POST api/<ChatsController>
    [HttpPost]
    public ActionResult Post([FromBody] Chat chat)
    {
        try
        {
            _chatService.CreateChat(chat);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete]
    [Route("{chatId}")]
    public ActionResult Delete(string chatId)
    {
        try
        {
            if (!_chatService.ChatExists(chatId)) return NotFound();
            _chatService.DeleteChat(chatId);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut]
    [Route("{chatId}")]
    public ActionResult Update([FromBody] Chat chat)
    {
        try
        {
            if (!_chatService.ChatExists(chat.Id)) return NotFound();
            _chatService.UpdateChat(chat);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}