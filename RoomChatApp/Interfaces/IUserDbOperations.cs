using RoomChatApp.Models;

namespace RoomChatApp.Interfaces;

public interface IUserDbOperations : IDbOperations<User>
{
    List<User> GetUsersWithUsername(string username);
}