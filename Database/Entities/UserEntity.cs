using Infrastructure.Models;

namespace Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(
        long id, 
        DateTime createdAt, 
        DateTime updatedAt, 
        Name name, 
        Email email, 
        string hashedPassword, 
        string avatarPath
        ) : base(id, createdAt, updatedAt)
    {
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
        AvatarPath = avatarPath;
    }

    public UserEntity()
    {
    }

    public Name Name { get; set; }
    
    public Email Email { get; set; }

    public string HashedPassword { get; set; } = string.Empty;
    
    public string AvatarPath { get; set; } = string.Empty;
}