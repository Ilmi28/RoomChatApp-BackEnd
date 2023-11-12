using Moq;
using RoomChatApp.Interfaces;
using RoomChatApp.Models;
using RoomChatApp.Services;

namespace RoomChatAppTests;

[TestFixture]
public class ChatServiceTests
{
    [SetUp]
    public void Setup()
    {
        _fakeDbOperations = new Mock<IChatDbOperations>();
        _chats = new List<Chat?>();
        _chatService = new ChatService(_fakeDbOperations.Object);

        _fakeDbOperations.Setup(x => x.Create(It.IsAny<Chat>()))
            .Callback((Chat? chat) => _chats.Add(chat));

        _fakeDbOperations.Setup(x => x.Get(It.IsAny<string>()))
            .Returns((string chatId) => _chats.FirstOrDefault(x => x.Id == chatId));

        _fakeDbOperations.Setup(x => x.Remove(It.IsAny<string>()))
            .Callback((string chatId) =>
            {
                var chat = _chats.FirstOrDefault(x => x.Id == chatId);
                _chats.Remove(chat);
            });
    }

    private Mock<IChatDbOperations> _fakeDbOperations = null!;
    private ChatService _chatService = null!;
    private List<Chat?> _chats = null!;


    [Test]
    public void GetChat_ReturnsChat()
    {
        var chatId = "1";
        _chats.Add(new Chat { Id = "1" });

        var chat = _chatService.GetChat(chatId);
        Assert.That(chat.Id, Is.EqualTo(chatId));
    }

    [Test]
    public void GetChat_ThrowsNullArgumentException()
    {
        Assert.That(() => _chatService.GetChat("1"), Throws.ArgumentNullException);
    }

    [Test]
    public void CreateChat_AddsChatToDatabase()
    {
        var newChat = new Chat();

        _chatService.CreateChat(newChat);

        Assert.That(_chats.Count, Is.EqualTo(1));
    }

    [Test]
    public void CreateChat_ThrowsArgumentNullException()
    {
        Assert.That(() => _chatService.CreateChat(null!), Throws.ArgumentNullException);
    }

    [Test]
    public void DeleteChat_DeletesChatFromDatabase()
    {
        _chats.Add(new Chat { Id = "1" });
        _chatService.DeleteChat("1");

        Assert.That(_chats.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteChat_ThrowsArgumentNullException()
    {
        Assert.That(() => _chatService.DeleteChat("1"), Throws.ArgumentNullException);
    }

    [Test]
    public void UpdateChat_UpdatesChatInDatabase()
    {
        var chat = new Chat { Id = "1", ChatName = "Chat 1" };
        _chats.Add(chat);
        var updatedChat = new Chat { Id = chat.Id, ChatName = "Chat 2" };
        _chatService.UpdateChat(updatedChat);
        var chat1 = _chats.FirstOrDefault(x => x.Id == "1");
        Assert.That(chat1.ChatName, Is.EqualTo("Chat 2"));
    }


    [Test]
    public void UpdateChat_ThrowsArgumentNullException()
    {
        Assert.That(() => _chatService.UpdateChat(null), Throws.ArgumentNullException);
    }

    [Test]
    public void IsUserChatCreator_ReturnsTrue()
    {
        var chat = new Chat { Id = "1", UserId = "1" };
        _chats.Add(chat);
        var isUserChatCreator = _chatService.IsUserChatCreator(chat.Id, "1");

        Assert.That(isUserChatCreator, Is.True);
    }

    [Test]
    public void IsUserChatCreator_ReturnsFalse()
    {
        var chat = new Chat { Id = "1", UserId = "2" };
        _chats.Add(chat);
        var isUserChatCreator = _chatService.IsUserChatCreator(chat.Id, "1");

        Assert.That(isUserChatCreator, Is.False);
    }

    [Test]
    public void IsUserChatCreator_ThrowsArgumentNullException()
    {
        Assert.That(() => _chatService.IsUserChatCreator("1", "1"), Throws.ArgumentNullException);
    }

    [Test]
    public void ChatExists_ReturnsTrue()
    {
        _chats.Add(new Chat { Id = "1" });
        Assert.That(_chatService.ChatExists("1"), Is.True);
    }

    [Test]
    public void ChatExists_ReturnsFalse()
    {
        Assert.That(_chatService.ChatExists("1"), Is.False);
    }
}