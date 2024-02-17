namespace SorokChatServer.Interfaces
{
    public interface ICookieService
    {
        public string Get(string key);
        void Set(string key, string value);
        void Delete(string key);
    }
}
