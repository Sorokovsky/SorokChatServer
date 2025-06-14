using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Models;

public class Password : ValueObject
{
    public const int MinLength = 6;

    private Password(string value)
    {
        Value = value;
    }

    public static string Message => $"Password must have length more than {MinLength} characters.";

    public string Value { get; }

    public static Result<Password, string> Create(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < MinLength)
            return Result.Failure<Password, string>(Message);

        return Result.Success<Password, string>(new Password(password));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}