using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Models;

public class Email : ValueObject
{
    public const string ErrorMessage = "Email is invalid";

    private const string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, string> Create(string email)
    {
        var regex = new Regex(Pattern);
        if (string.IsNullOrEmpty(email) || !regex.IsMatch(email)) return Result.Failure<Email, string>(ErrorMessage);

        return Result.Success<Email, string>(new Email(email));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}