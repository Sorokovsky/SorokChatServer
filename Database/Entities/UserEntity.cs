using Infrastructure.Models;

namespace Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id, DateTime createdAt, DateTime updatedAt, FullName fullName, string email, string password, string avatarPath) : base(id, createdAt, updatedAt)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        AvatarPath = avatarPath;
    }

    public UserEntity()
    {
    }

    public FullName FullName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string AvatarPath { get; set; }
}