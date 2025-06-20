using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Infrastructure.Models;

public class Email : ValueObject
{
    private const string Regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    public const string Message = "Invalid email format.";
    
    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email, string> Create(string email)
    {
        var regex = new Regex(Regex);
        var match = regex.Match(email);
        var successResult = Result.Success<Email, string>(new Email(email));
        var failureResult = Result.Failure<Email, string>(Message);
        return match.Success ? successResult : failureResult; 
    }

    public string Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}