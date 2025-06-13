using SorokChatServer.Application.Model;

namespace SorokChatServer.Application.Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(FullName fullName, Email email, Password password, AvatarPath avatarPath)
    {
        FirstName = fullName.FirstName;
        LastName = fullName.LastName;
        MiddleName = fullName.MiddleName;
        Email = email.Value;
        Password = password.Value;
        AvatarPath = avatarPath.Value;
    }

    private UserEntity()
    {
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string AvatarPath { get; set; }
}