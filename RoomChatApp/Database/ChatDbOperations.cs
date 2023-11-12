using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatApp.Database;

public class ChatDbOperations : IChatDbOperations
{
    private readonly RoomChatDbContext _context;

    public ChatDbOperations(RoomChatDbContext context)
    {
        _context = context;
    }


    public void Create(Chat chat)
    {
        _context.Chats.Add(chat);
        _context.SaveChanges();
    }


    public void Remove(string chatId)
    {
        var chat = _context.Chats.FirstOrDefault(x => x.Id == chatId) ?? throw new ArgumentNullException();
        _context.Chats.Remove(chat);
        _context.SaveChanges();
    }

    public Chat Get(string chatId)
    {
        var chat = _context.Chats.FirstOrDefault(x => x.Id == chatId) ?? throw new ArgumentNullException();
        return chat;
    }
}