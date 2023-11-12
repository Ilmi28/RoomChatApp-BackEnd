 using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatApp.Services;

public class ChatService : IChatService
{
    private readonly IChatDbOperations _chatDbOperations;

    public ChatService(IChatDbOperations chatDbOperations)
    {
        _chatDbOperations = chatDbOperations;
    }

    public Chat GetChat(string chatId)
    {
        var chat = _chatDbOperations.Get(chatId) ?? throw new ArgumentNullException();
        return chat;
    }

    public void CreateChat(Chat chat)
    {
        chat = chat ?? throw new ArgumentNullException();
        var newChat = new Chat
        {
            Id = Guid.NewGuid().ToString(),
            ChatName = chat.ChatName,
            IsPrivate = chat.IsPrivate,
            Password = chat.Password,
            UserId = chat.UserId
        };
        _chatDbOperations.Create(newChat);
    }

    public void DeleteChat(string chatId)
    {
        var chat = _chatDbOperations.Get(chatId) ?? throw new ArgumentNullException();
        _chatDbOperations.Remove(chatId);
    }

    public void UpdateChat(Chat updatedChat)
    {
        updatedChat = updatedChat ?? throw new ArgumentNullException();
        updatedChat.Id = updatedChat.Id ?? throw new ArgumentNullException();
        _chatDbOperations.Remove(updatedChat.Id);
        var newChat = new Chat
        {
            Id = updatedChat.Id,
            ChatName = updatedChat.ChatName,
            IsPrivate = updatedChat.IsPrivate,
            Password = updatedChat.Password
        };
        _chatDbOperations.Create(newChat);
    }

    public bool IsUserChatCreator(string chatId, string userId)
    {
        var chat = _chatDbOperations.Get(chatId) ?? throw new ArgumentNullException();
        return chat.UserId == userId;
    }

    public bool ChatExists(string chatId)
    {
        return _chatDbOperations.Get(chatId) != null;
    }
}