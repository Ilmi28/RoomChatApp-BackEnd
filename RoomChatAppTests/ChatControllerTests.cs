using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomChatApp.Controllers;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatAppTests;

[TestFixture]
public class ChatControllerTests
{
    [SetUp]
    public void SetUp()
    {
        _chatService = new Mock<IChatService>();
        _userService = new Mock<IUserService>();
        _chatsController = new ChatsController(_chatService.Object, _userService.Object);
    }

    private ChatsController _chatsController = null!;
    private Mock<IChatService> _chatService = null!;
    private Mock<IUserService> _userService = null!;

    [Test]
    public void Get_ReturnsNotFoundResult()
    {
        _chatService.Setup(x => x.GetChat(It.IsAny<string>())).Throws(new ArgumentNullException());

        Assert.That(_chatsController.Get("1"), Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Get_ReturnsOkObjectResult()
    {
        var chat = new Chat { Id = "1" };
        _chatService.Setup(x => x.GetChat(It.IsAny<string>())).Returns(chat);
        _chatService.Setup(x => x.ChatExists(It.IsAny<string>())).Returns(true);

        Assert.That(_chatsController.Get("1"), Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void Post_ReturnsBadRequestObjectResult()
    {
        _chatService.Setup(x => x.CreateChat(It.IsAny<Chat>())).Throws(new ArgumentNullException());

        Assert.That(_chatsController.Post(new Chat()), Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void Post_ReturnsOkResult()
    {
        _chatService.Setup(x => x.CreateChat(It.IsAny<Chat>())).Callback(() => { });

        Assert.That(_chatsController.Post(new Chat()), Is.InstanceOf<OkResult>());
    }


    [Test]
    public void Delete_ReturnsNotFoundResult()
    {
        _chatService.Setup(x => x.ChatExists(It.IsAny<string>())).Returns(false);

        var result = _chatsController.Delete("1");

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Delete_ReturnsOkResult()
    {
        _chatService.Setup(x => x.ChatExists(It.IsAny<string>())).Returns(true);

        var result = _chatsController.Delete("1");

        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public void Update_ReturnsNotFoundResult()
    {
        _chatService.Setup(x => x.ChatExists(It.IsAny<string>())).Returns(false);

        var result = _chatsController.Update(new Chat());

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Update_ReturnsOkResult()
    {
        _chatService.Setup(x => x.ChatExists(It.IsAny<string>())).Returns(true);

        var result = _chatsController.Update(new Chat());

        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}