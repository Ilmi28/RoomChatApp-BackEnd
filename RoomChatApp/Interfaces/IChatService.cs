using RoomChatApp.Models;

namespace RoomChatApp.Interfaces;

public interface IChatService
{
    Chat GetChat(string chatId);
    void CreateChat(Chat chat);
    void DeleteChat(string chatId);
    void UpdateChat(Chat updatedChat);
    bool IsUserChatCreator(string chatId, string userId);
    bool ChatExists(string chatId);
}