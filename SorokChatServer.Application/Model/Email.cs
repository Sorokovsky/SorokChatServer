using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Model;

public class Email : ValueObject
{
    public const string ErrorMessage = "Email is invalid";
    private const string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
    
    private Email(string value)
    {
        Value = value;
    }

    public Result<Email, string> Create(string email)
    {
        var regex = new Regex(Pattern);
        if (string.IsNullOrEmpty(email) || !regex.IsMatch(email))
        {
            return Result.Failure<Email, string>(ErrorMessage);
        }

        return Result.Success<Email, string>(new Email(email));
    }

    public string Value { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}