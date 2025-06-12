using SorokChatServer.Application.Model;

namespace SorokChatServer.Application.Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id, DateTime createdAt, DateTime updatedAt, FullName fullName, Email email, Password password, AvatarPath avatarPath) 
        : base(id, createdAt, updatedAt)
    {
        Email = email;
        Password = password;
        FullName = fullName;
        AvatarPath = avatarPath;
    }

    public UserEntity()
    {
    }
    
    public FullName FullName { get; set; } 
    
    public Email Email { get; set; }
    
    public Password Password { get; set; }
    
    public AvatarPath AvatarPath { get; set; }
}