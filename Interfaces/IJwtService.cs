using SorokChatServer.Models;

namespace SorokChatServer.Interfaces
{
    public interface IJwtService
    {
        public string GenerateAccessToken<T>(T payload);
        public string GenerateRefreshToken<T>(T payload);
        public TokensModel GenerateTokens<T>(T payload);
        public T ExtractToken<T>(string token);
        public bool IsTokenValid(string token);
    }
}
