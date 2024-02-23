using SorokChatServer.Exceptions;
using SorokChatServer.Interfaces;
using SorokChatServer.Utils;

namespace SorokChatServer.Services
{
    public class BearerService : IBearerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _accessKey = "Authorization";

        public BearerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            HttpContextChecker.Check(_httpContextAccessor);
        }

        public void SetAccessToken(string token)
        {
            _httpContextAccessor.HttpContext.Response.Headers[_accessKey] = $"Bearer {token}";
        }

        public void DeleteAccessToken()
        {
            _httpContextAccessor.HttpContext.Response.Headers.Remove(_accessKey);
        }

        public string GetAccessToken()
        {
            string? authorization = _httpContextAccessor.HttpContext.Request.Headers[_accessKey];
            if (authorization == null)
            {
                throw new NotFoundException("Token undefined");
            }
            string[] splited = authorization.Split(' ');

            string accessToken = splited[1];
            return accessToken;
        }

    }
}