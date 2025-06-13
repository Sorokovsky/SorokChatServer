namespace SorokChatServer.Application.Database.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(long id,
        DateTime createdAt,
        DateTime updatedAt,
        string firstName,
        string lastName,
        string middleName,
        string email,
        string password,
        string avatarPath
    )
        : base(id, createdAt, updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;
        Password = password;
        AvatarPath = avatarPath;
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