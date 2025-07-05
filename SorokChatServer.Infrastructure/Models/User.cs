using CSharpFunctionalExtensions;
using SorokChatServer.Infrastructure.ValueObjects;

namespace SorokChatServer.Infrastructure.Models;

public class User : Base
{
    public User(
        long id,
        DateTime createdAt,
        DateTime updatedAt,
        Name name,
        Email email,
        Password password
    ) : base(id, createdAt, updatedAt)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public Name Name { get; }

    public Email Email { get; }

    public Password Password { get; }

    public static Result<User> Create(
        long id,
        DateTime createdAt,
        DateTime updatedAt,
        string firstName,
        string lastName,
        string middleName,
        string email,
        string password
    )
    {
        var nameResult = Name.Create(firstName, lastName, middleName);
        if (nameResult.IsFailure) return Result.Failure<User>(nameResult.Error);
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure) return Result.Failure<User>(emailResult.Error);
        var passwordResult = Password.Create(password);
        if (passwordResult.IsFailure) return Result.Failure<User>(passwordResult.Error);
        return Result.Success(new User(id, createdAt, updatedAt, nameResult.Value, emailResult.Value,
            passwordResult.Value));
    }

    public void SetHashedPassword(string hashedPassword)
    {
        Password.SetHashedPassword(hashedPassword);
    }
}