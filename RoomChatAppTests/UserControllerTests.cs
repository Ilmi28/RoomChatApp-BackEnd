using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomChatApp.Controllers;
using RoomChatApp.FormModels;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatAppTests;

[TestFixture]
public class UserControllerTests
{
    [SetUp]
    public void SetUp()
    {
        _userService = new Mock<IUserService>();
        _userController = new UserController(_userService.Object);
    }

    private Mock<IUserService> _userService = null!;
    private UserController _userController = null!;

    [Test]
    public void Login_ReturnsUnauthrozedResult()
    {
        _userService.Setup(x => x.TryLogin(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new UnauthorizedAccessException());

        var result = _userController.Login(new UserForm());

        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void Login_ReturnsOkResult()
    {
        _userService.Setup(x => x.TryLogin(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new User()); 

        var result = _userController.Login(new UserForm());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}