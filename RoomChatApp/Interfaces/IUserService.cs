using RoomChatApp.Models;

namespace RoomChatApp.Interfaces;

public interface IUserService
{
    void CreateUser(User user);
    User GetUser(string userId);
    void UpdateUser(User updatedUser);
    void DeleteUser(string userId);
    bool UserExists(string userId);
    User TryLogin(string username, string password);
}