using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Models;

public class FullName : ValueObject
{
    public const int MaxLength = 20;

    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string MiddleName { get; }

    public static Result<FullName, string> Create(string firstName, string lastName, string middleName)
    {
        if (string.IsNullOrEmpty(firstName) || firstName.Length > MaxLength)
            return Result.Failure<FullName, string>(GenerateErrorString(nameof(firstName)));

        if (string.IsNullOrEmpty(lastName) || lastName.Length > MaxLength)
            return Result.Failure<FullName, string>(GenerateErrorString(nameof(lastName)));

        if (string.IsNullOrEmpty(middleName) || middleName.Length > MaxLength)
            return Result.Failure<FullName, string>(GenerateErrorString(nameof(middleName)));

        return Result.Success<FullName, string>(new FullName(firstName, lastName, middleName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }

    private static string GenerateErrorString(string name)
    {
        return $"{name} must have length from 1 to {MaxLength}.";
    }
}