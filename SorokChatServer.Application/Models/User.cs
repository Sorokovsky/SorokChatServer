using CSharpFunctionalExtensions;
using SorokChatServer.Application.Contracts.Users;
using SorokChatServer.Application.Database.Entities;

namespace SorokChatServer.Application.Models;

public class User : Base
{
    private User(long id, DateTime createdAt, DateTime updatedAt, FullName fullName, Email email, Password password,
        AvatarPath avatarPath)
        : base(id, createdAt, updatedAt)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        AvatarPath = avatarPath;
    }

    public FullName FullName { get; }

    public Email Email { get; }

    public Password Password { get; }

    public AvatarPath AvatarPath { get; }

    public GetUser ToGetUser()
    {
        return new GetUser(
            Id,
            CreatedAt,
            UpdatedAt,
            FullName.FirstName,
            FullName.LastName,
            FullName.MiddleName,
            Email.Value,
            AvatarPath.Value
        );
    }

    public static Result<User, string> Create(
        long id,
        DateTime createdAt,
        DateTime updatedAt,
        string firstName,
        string lastName,
        string middleName,
        string email,
        string password,
        string avatarPath
    )
    {
        var fullNameResult = FullName.Create(firstName, lastName, middleName);
        if (fullNameResult.IsFailure) return Result.Failure<User, string>(fullNameResult.Error);
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure) return Result.Failure<User, string>(emailResult.Error);
        var passwordResult = Password.Create(password);
        if (passwordResult.IsFailure) return Result.Failure<User, string>(passwordResult.Error);
        var avatarPathResult = AvatarPath.Create(avatarPath);
        if (avatarPathResult.IsFailure) return Result.Failure<User, string>(avatarPathResult.Error);
        var user = new User(id, createdAt, updatedAt, fullNameResult.Value, emailResult.Value, passwordResult.Value,
            avatarPathResult.Value);
        return Result.Success<User, string>(user);
    }

    public static Result<User, string> Create(CreateUser createUser)
    {
        return Create(
            0,
            DateTime.UtcNow,
            DateTime.UtcNow,
            createUser.FirstName,
            createUser.LastName,
            createUser.MiddleName,
            createUser.Email,
            createUser.Password,
            createUser.AvatarPath
        );
    }

    public UserEntity ToEntity()
    {
        return new UserEntity(
            Id,
            CreatedAt,
            UpdatedAt,
            FullName.FirstName,
            FullName.LastName,
            FullName.MiddleName,
            Email.Value,
            Password.Value,
            AvatarPath.Value
        );
    }

    public static User FromEntity(UserEntity entity)
    {
        var fullNameResult = FullName.Create(entity.FirstName, entity.LastName, entity.MiddleName);
        var emailResult = Email.Create(entity.Email);
        var passwordResult = Password.Create(entity.Password);
        var avatarPathResult = AvatarPath.Create(entity.AvatarPath);
        return new User(entity.Id, entity.CreatedAt, entity.UpdatedAt, fullNameResult.Value, emailResult.Value,
            passwordResult.Value, avatarPathResult.Value);
    }
}