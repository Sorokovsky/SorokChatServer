using CSharpFunctionalExtensions;

namespace Infrastructure.Models;

public class Name : ValueObject
{
    public const int MaxSize = 20;
    
    private Name(string first, string last, string middle)
    {
        First = first;
        Last = last;
        Middle = middle;
    }

    public static Result<Name, string> Create(string firstName, string lastName, string middleName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MaxSize)
        {
            return Result.Failure<Name, string>("First name is too long or empty");
        }

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MaxSize)
        {
            return Result.Failure<Name, string>("Last name is too long or empty");
        }

        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > MaxSize)
        {
            return Result.Failure<Name, string>("Middle name is too long or empty");
        }
        return Result.Success<Name, string>(new Name(firstName, lastName, middleName));
    }

    public string First { get; }
    
    public string Last { get; }
    
    public string Middle { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
        yield return Middle;
    }
}