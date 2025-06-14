using SorokChatServer.Application.Interfaces;

namespace SorokChatServer.Application.Services;

public class BcryptPasswordService : IPasswordService
{
    public string Hash(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public bool IsPasswordCorrect(string plainPassword, string encryptedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, encryptedPassword);
    }
}