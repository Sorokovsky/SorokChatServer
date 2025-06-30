using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace SorokChatServer.Infrastructure.ValueObjects;

public partial class Email : ValueObject
{
    public const int MaxLength = 100;

    public const string EmailTemplate =
        @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        var regex = EmailRegex();
        if (string.IsNullOrEmpty(email) || !regex.Match(email).Success)
            return Result.Failure<Email>("Invalid email format");

        return Result.Success(new Email(email));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(EmailTemplate)]
    private static partial Regex EmailRegex();
}