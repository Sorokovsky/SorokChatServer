using CSharpFunctionalExtensions;

namespace Infrastructure.Models;

public class FullName : ValueObject
{
    public const int MaxSize = 20;
    
    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public static Result<FullName, string> Create(string firstName, string lastName, string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MaxSize)
        {
            return Result.Failure<FullName, string>("First name is too long or empty");
        }

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MaxSize)
        {
            return Result.Failure<FullName, string>("Last name is too long or empty");
        }

        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > MaxSize)
        {
            return Result.Failure<FullName, string>("Middle name is too long or empty");
        }
        return Result.Success<FullName, string>(new FullName(firstName, lastName, middleName));
    }

    public string FirstName { get; }
    
    public string LastName { get; }
    
    public string MiddleName { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}