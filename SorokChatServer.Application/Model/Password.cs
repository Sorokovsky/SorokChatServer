using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Model;

public class Password : ValueObject
{
    public const int MinLength = 6;
    public const int MaxLength = 20;
    public static string Message => $"Password must have length between {MinLength} and {MaxLength} characters.";
    
    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password, string> Create(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < MinLength || password.Length > MaxLength)
        {
            return Result.Failure<Password, string>(Message);
        }

        return Result.Success<Password, string>(new Password(password));
    }

    public string Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}