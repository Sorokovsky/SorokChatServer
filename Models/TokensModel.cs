namespace SorokChatServer.Models
{
    public class TokensModel
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        public TokensModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
