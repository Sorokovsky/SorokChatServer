using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Model;

public class AvatarPath : ValueObject
{
    public const string Default = "Common/avatar.png";
    public const string Message = "Avatar Path invalid.";

    private AvatarPath(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<AvatarPath, string> Create(string value)
    {
        if (string.IsNullOrEmpty(value)) return Result.Failure<AvatarPath, string>(Message);
        return Result.Success<AvatarPath, string>(new AvatarPath(value));
    }

    public static AvatarPath GetDefault() => new AvatarPath(Default);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}