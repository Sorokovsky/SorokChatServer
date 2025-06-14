namespace SorokChatServer.Application.Interfaces;

public interface IPasswordService
{
    public string Hash(string plainPassword);

    public bool IsPasswordCorrect(string plainPassword, string encryptedPassword);
}