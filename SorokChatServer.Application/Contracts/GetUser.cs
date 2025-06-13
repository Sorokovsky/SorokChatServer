namespace SorokChatServer.Application.Contracts;

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