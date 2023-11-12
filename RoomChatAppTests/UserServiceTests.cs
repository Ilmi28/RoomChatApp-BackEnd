using Moq;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;
using RoomChatApp.Services;
using System.Security.Cryptography;

namespace RoomChatAppTests;

[TestFixture]
public class UserServiceTests
{
    [SetUp]
    public void Setup()
    {
        _users = new List<User>();
        _dbOperations = new Mock<IUserDbOperations>();
        _userService = new UserService(_dbOperations.Object);

        _dbOperations.Setup(x => x.Create(It.IsAny<User>()))
            .Callback((User user) => _users.Add(user));
        _dbOperations.Setup(x => x.Get(It.IsAny<string>()))
            .Returns((string userId) => _users.FirstOrDefault(x => x.Id == userId));
        _dbOperations.Setup(x => x.Remove(It.IsAny<string>()))
            .Callback((string userId) =>
            {
                var user = _users.FirstOrDefault(x => x.Id == userId);
                _users.Remove(user);
            });
        _dbOperations.Setup(x => x.GetUsersWithUsername(It.IsAny<string>()))
            .Returns((string username) => _users.Where(x => x.Username == username).ToList());
    }

    private List<User?> _users = null!;
    private UserService _userService = null!;
    private Mock<IUserDbOperations> _dbOperations = null!;

    [Test]
    public void CreateUser_AddsUserToDatabase()
    {
        var user = new User { Id = "1" };
        _userService.CreateUser(user);
        Assert.That(_users.Count, Is.EqualTo(1));
    }

    [Test]
    public void CreateUser_ThrowsArgumentNullException()
    {
        Assert.That(() => _userService.CreateUser(null), Throws.ArgumentNullException);
    }

    [Test]
    public void GetUser_ReturnsUser()
    {
        var user = new User { Id = "1" };
        _users.Add(user);
        Assert.That(_userService.GetUser("1").Id, Is.EqualTo("1"));
    }

    [Test]
    public void GetUser_ThrowsArgumentNullException()
    {
        Assert.That(() => _userService.GetUser("1"), Throws.ArgumentNullException);
    }

    [Test]
    public void UpdateUser_UpdatesUserInDatabase()
    {
        var user = new User { Id = "1", Username = "aaa" };
        _users.Add(user);
        var updatedUser = new User { Id = user.Id, Username = "abc" };
        _userService.UpdateUser(updatedUser);
        var dbUser = _users.FirstOrDefault(x => x.Id == "1");

        Assert.That(dbUser.Username, Is.EqualTo("abc"));
    }


    [Test]
    public void UpdateUser_ThrowsArgumentNullException()
    {
        var user = new User { Id = "1" };
        _users.Add(user);
        var newUser = new User { Id = "2" };
        Assert.That(() => _userService.UpdateUser(null), Throws.ArgumentNullException);
        Assert.That(() => _userService.UpdateUser(newUser), Throws.ArgumentNullException);
    }


    [Test]
    public void DeleteUser_DeletesUserFromDatabase()
    {
        var user = new User { Id = "1" };
        _users.Add(user);

        _userService.DeleteUser("1");

        Assert.That(_users.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteUser_ThrowsArgumentNullException()
    {
        Assert.That(() => _userService.DeleteUser("1"), Throws.ArgumentNullException);
    }

    [Test]
    public void UserExists_ReturnsTrue()
    {
        var user = new User { Id = "1" };
        _users.Add(user);
        var result = _userService.UserExists("1");
        Assert.That(result, Is.True);
    }

    [TestCase(null)]
    [TestCase("1")]
    [Test]
    public void UserExists_ReturnsFalse(string userId)
    {
        var result = _userService.UserExists(userId);
        Assert.That(result, Is.False);
    }

    [Test]
    public void TryLogin_ReturnsUser()
    {
        var user = new User
        {
            Id = "1",
            Username = "John",
            Password = "123"
        };
        var user2 = new User
        {
            Id = "2",
            Username = "John",
            Password = "qwerty"
        };
        _users.Add(user);
        _users.Add(user2);

        var result = _userService.TryLogin("John", "qwerty");
        Assert.That(result.Id, Is.EqualTo("2"));
    }

    [Test]
    public void TryLogin_ThrowsUnauthorizedAccessException()
    {
        var user = new User
        {
            Id = "1",
            Username = "John",
            Password = "123"
        };

        _users.Add(user);

        Assert.That(() => _userService.TryLogin("John", "1234"), Throws.InstanceOf<UnauthorizedAccessException>());
    }




}