using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private const int CookieMaxAge = 7;

        private readonly IHttpContextAccessor _contextAccessor;

        public CookieService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetCookie(string key)
        {
            IRequestCookieCollection cookies = _contextAccessor.HttpContext.Request.Cookies;
            string cookie;
            if(cookies.TryGetValue(key, out cookie))
            {
                return cookie;
            }
            else
            {
                return null;
            }
        }

        public void SetCookie(string key, string value)
        {
            IResponseCookies cookies = _contextAccessor.HttpContext.Response.Cookies;
            CookieOptions cookie = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(CookieMaxAge),
            };
            cookies.Append(key, value, cookie);

        }
    }
}
