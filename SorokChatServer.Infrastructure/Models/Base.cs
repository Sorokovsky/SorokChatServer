namespace SorokChatServer.Infrastructure.Models;

public class Base
{
    public Base(long id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public long Id { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }
}