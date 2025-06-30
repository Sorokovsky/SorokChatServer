using SorokChatServer.Infrastructure.ValueObjects;

namespace SorokChatServer.Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id, DateTime createdAt, DateTime updatedAt, Name name) : base(id, createdAt, updatedAt)
    {
        Name = name;
    }

    public UserEntity()
    {
    }

    public Name Name { get; set; } = null!;
}