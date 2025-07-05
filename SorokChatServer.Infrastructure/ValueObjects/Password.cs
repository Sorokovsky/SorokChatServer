using CSharpFunctionalExtensions;

namespace SorokChatServer.Infrastructure.ValueObjects;

public class Password : ValueObject
{
    public const int MinLength = 8;
    public const int MaxLength = 256;

    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < MinLength || password.Length > MaxLength)
            return Result.Failure<Password>($"Password must be between {MinLength} and {MaxLength} characters");

        return Result.Success(new Password(password));
    }

    public void SetHashedPassword(string hashedPassword)
    {
        Value = hashedPassword;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}