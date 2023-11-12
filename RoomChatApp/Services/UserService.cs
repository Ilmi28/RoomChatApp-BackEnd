using Microsoft.Net.Http.Headers;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;
using System.Net;

namespace RoomChatApp.Services;

public class UserService : IUserService
{
    private readonly IUserDbOperations _userDbOperations;

    public UserService(IUserDbOperations userDbOperations)
    {
        _userDbOperations = userDbOperations;
    }

    public void CreateUser(User user)
    {
        user = user ?? throw new ArgumentNullException();
        _userDbOperations.Create(user);
    }

    public User GetUser(string userId)
    {
        var user = _userDbOperations.Get(userId) ?? throw new ArgumentNullException();
        return user;
    }

    public void UpdateUser(User updatedUser)
    {
        updatedUser = updatedUser ?? throw new ArgumentNullException();
        if (!UserExists(updatedUser.Id ?? throw new ArgumentNullException()))
            throw new ArgumentNullException();

        _userDbOperations.Remove(updatedUser.Id ?? throw new ArgumentNullException());
        var newUser = new User
        {
            Id = updatedUser.Id,
            Username = updatedUser.Username,
            Password = updatedUser.Password
        };
        _userDbOperations.Create(newUser);
    }

    public void DeleteUser(string userId)
    {
        var user = _userDbOperations.Get(userId) ?? throw new ArgumentNullException();
        _userDbOperations.Remove(user.Id ?? throw new ArgumentNullException());
    }

    public bool UserExists(string userId)
    {
        try
        {
            return _userDbOperations.Get(userId) != null;
        }
        catch { return false; }
    }

    public User TryLogin(string username, string password)
    {
        var users = _userDbOperations.GetUsersWithUsername(username);
        foreach (var user in users)
        {
            if (user.Password == password)
            {
                return user;
            }
        }

        throw new UnauthorizedAccessException();
    }
}