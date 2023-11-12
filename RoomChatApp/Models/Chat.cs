using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomChatApp.Models;

public class Chat
{
    [Key] public string? Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string ChatName { get; set; } = null!;

    [Required] public bool IsPrivate { get; set; }

    [MinLength(3)] [MaxLength(50)] public string? Password { get; set; }
    public User User { get; set; }

    [Required] [ForeignKey("User")] public string UserId { get; set; } = null!;
}