using Microsoft.AspNetCore.Authorization;
using SorokChatServer.Interfaces;
using SorokChatServer.Models;

namespace SorokChatServer.Authorization
{
    public class AuthorizationCustomHandler : AuthorizationHandler<AuthorizationRequerment>
    {

        private readonly IJwtService _jwtService;
        private readonly IBearerService _bearerService;
        private readonly ICookieService _cookieService;

        public AuthorizationCustomHandler(
            IJwtService jwtService, 
            IBearerService bearerService, 
            ICookieService cookieService
            )
        {
            _jwtService = jwtService;
            _bearerService = bearerService;
            _cookieService = cookieService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequerment requirement)
        {
                string accessToken = _bearerService.GetAccessToken();
                bool isTokenValid = _jwtService.IsTokenValid(accessToken);
                if (isTokenValid == false)
                {
                    string refreshToken = _cookieService.Get("refresh_token");
                    bool isValid = _jwtService.IsTokenValid(refreshToken);
                    if (isValid == false)
                    {
                        context.Fail();
                    }
                    else
                    {
                        UsersModel model = _jwtService.ExtractToken<UsersModel>(refreshToken);
                        TokensModel tokens = _jwtService.GenerateTokens(model);
                        _bearerService.SetAccessToken(tokens.AccessToken);
                        _cookieService.Set("refresh_token", tokens.RefreshToken);
                        context.Succeed(requirement);
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
        }
    }
}
