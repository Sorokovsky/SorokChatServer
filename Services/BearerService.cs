using SorokChatServer.Interfaces;

namespace SorokChatServer.Services
{
    public class BearerService : IBearerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _accessKey = "Authorization";

        public BearerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetAccessToken(string token)
        {
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext, nameof(_httpContextAccessor.HttpContext));
            _httpContextAccessor.HttpContext.Response.Headers[_accessKey] = $"Bearer {token}";
        }

        public void DeleteAccessToken()
        {
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext, nameof(_httpContextAccessor.HttpContext));
            _httpContextAccessor.HttpContext.Response.Headers.Remove(_accessKey);
        }

        public string GetAccessToken()
        {
            string? authorization = _httpContextAccessor.HttpContext.Request.Headers[_accessKey];
            string[] splited = authorization.Split(' ');

            string accessToken = splited[1];
            return accessToken;
        }
    }
}