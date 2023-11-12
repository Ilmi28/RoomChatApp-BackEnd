using Microsoft.EntityFrameworkCore;

namespace RoomChatApp.Models;

public class RoomChatDbContext : DbContext
{
    public RoomChatDbContext(DbContextOptions<RoomChatDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
}