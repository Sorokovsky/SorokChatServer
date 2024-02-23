using Microsoft.AspNetCore.Authorization;
using SorokChatServer.Exceptions;
using SorokChatServer.Interfaces;
using SorokChatServer.Models;
using SorokChatServer.Models.Users;

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
            bool isTokenValid;
            try
            {
                string accessToken = _bearerService.GetAccessToken();
                isTokenValid = _jwtService.IsTokenValid(accessToken);
            }
            catch
            {
                isTokenValid = false;
            }
            if (isTokenValid == false)
            {
                string refreshToken = _cookieService.Get("refresh_token");
                bool isValid = _jwtService.IsTokenValid(refreshToken);
                if (isValid == false)
                {
                    throw new UnauthorizationException();
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
