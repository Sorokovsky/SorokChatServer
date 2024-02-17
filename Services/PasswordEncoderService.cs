using SorokChatServer.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SorokChatServer.Services
{
    public class PasswordEncoderService : IPasswordEncoderService
    {
        public string Encode(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public bool Verify(string password, string encodedPassword)
        {
            string encodedInput = Encode(password);
            return encodedInput == encodedPassword;
        }
    }
}
