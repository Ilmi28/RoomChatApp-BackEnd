namespace RoomChatApp.Interfaces;

public interface IDbOperations<T>
{
    void Create(T element);
    void Remove(string id);
    T Get(string id);
}