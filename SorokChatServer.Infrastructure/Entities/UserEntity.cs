using SorokChatServer.Infrastructure.ValueObjects;

namespace SorokChatServer.Infrastructure.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id, DateTime createdAt, DateTime updatedAt, Name name, Email email, Password password)
        : base(id, createdAt, updatedAt)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public UserEntity()
    {
    }

    public Name Name { get; set; } = null!;

    public Email Email { get; set; } = null!;

    public Password Password { get; set; } = null!;
}