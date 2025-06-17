using CSharpFunctionalExtensions;

namespace Infrastructure.Models;

public class Password : ValueObject
{
    public const int MinLength = 6;
    public const int MaxLength = 64;
    
    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password, string> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < MinLength || password.Length > MaxLength)
        {
            return Result.Failure<Password, string>($"Password must have length of {MinLength} and {MaxLength} characters.");
        }
        return Result.Success<Password, string>(new Password(password));
    }

    public string Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}