namespace SorokChatServer.Application.Contracts.Users;

public record CreateUser(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string MiddleName,
    string AvatarPath);