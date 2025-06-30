using CSharpFunctionalExtensions;

namespace SorokChatServer.Infrastructure.ValueObjects;

public class Name : ValueObject
{
    public const int MaxFirstNameLength = 20;
    public const int MaxLastNameLength = 20;
    public const int MaxMiddleNameLength = 20;

    private Name(string first, string last, string middle)
    {
        First = first;
        Last = last;
        Middle = middle;
    }

    public string First { get; }

    public string Last { get; }

    public string Middle { get; }

    public static Result<Name> Create(string first, string last, string middle)
    {
        if (string.IsNullOrEmpty(first) || first.Length >= MaxFirstNameLength)
            return Result.Failure<Name>($"{nameof(first)} must not be empty or longer than {MaxFirstNameLength})");

        if (string.IsNullOrEmpty(last) || last.Length >= MaxLastNameLength)
            return Result.Failure<Name>($"{nameof(last)} must not be empty or longer than {MaxLastNameLength})");

        if (string.IsNullOrEmpty(middle) || middle.Length >= MaxMiddleNameLength)
            return Result.Failure<Name>($"{nameof(middle)} must not be empty or longer than {MaxMiddleNameLength})");

        return Result.Success(new Name(first, last, middle));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
        yield return Middle;
    }
}