namespace SorokChatServer.Interfaces
{
    public interface IBearerService
    {
        public void SetAccessToken(string token);
        public void DeleteAccessToken();
    }
}
