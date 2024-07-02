
namespace Infrastructure.Interfaces
{
    public interface ICookieService
    {
        public void SetCookie(string key, string value);

        public string? GetCookie(string key);
    }
}
