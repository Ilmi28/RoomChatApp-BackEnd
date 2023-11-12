using RoomChatApp.Interfaces;
using RoomChatApp.Models;

namespace RoomChatApp.Services;

public class UserDbOperations : IUserDbOperations
{
    private readonly RoomChatDbContext _context;

    public UserDbOperations(RoomChatDbContext context)
    {
        _context = context;
    }

    public void Create(User user)
    {
        user = user ?? throw new ArgumentNullException();
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Remove(string id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException();
        _context.Remove(user);
        _context.SaveChanges();
    }

    public User Get(string id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException();
        return user;
    }

    public List<User> GetUsersWithUsername(string username)
    {
        var users = _context.Users.Where(x => x.Username == username).ToList();
        return users;
    }
}