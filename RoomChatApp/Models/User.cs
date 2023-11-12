using System.ComponentModel.DataAnnotations;

namespace RoomChatApp.Models;

public class User
{
    [Key] public string Id { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
}