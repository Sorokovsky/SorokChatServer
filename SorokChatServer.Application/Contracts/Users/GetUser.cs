namespace SorokChatServer.Application.Contracts.Users;

public record GetUser(
    long Id,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string AvatarPath
);