using CSharpFunctionalExtensions;

namespace SorokChatServer.Application.Model;

public class AvatarPath : ValueObject
{
    private AvatarPath(string avatarPath)
    {
        Value = avatarPath;
    }

    public const string Default = "Common/avatar.png";
    public const string Message = "Avatar Path invalid.";
    public string Value { get; }

    public static Result<AvatarPath, string> Create(string avatarPath)
    {
        if(string.IsNullOrEmpty(avatarPath)) return Result.Failure<AvatarPath, string>(Message);
        return Result.Success<AvatarPath, string>(new AvatarPath(avatarPath));
    }

    public static AvatarPath GetDefault() => new AvatarPath(Default);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}