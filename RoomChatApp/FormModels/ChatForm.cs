namespace RoomChatApp.FormModels
{
    public class ChatForm
    {
        public string ChatName { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public string? Password { get; set; }

    }
}
