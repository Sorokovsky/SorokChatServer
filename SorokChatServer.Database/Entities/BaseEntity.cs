namespace SorokChatServer.Database.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(long id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    protected BaseEntity()
    {
    }

    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}