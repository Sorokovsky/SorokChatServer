using Microsoft.AspNetCore.Http;
using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;
using SorokChatServer.Mappers;
using SorokChatServer.Models;

namespace SorokChatServer.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersService _usersService;
        private readonly IJwtService _jwtService;
        private readonly IPasswordEncoderService _passwordEncoderService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _refreshKey = "refresh_token";
        private readonly string _accessKey = "Authorization";

        public AuthorizationService(
            IUsersService usersService, 
            IJwtService jwtService, 
            IPasswordEncoderService passwordEncoderService, 
            ICookieService cookieService, 
            IHttpContextAccessor httpContextAccessor
            )
        {
            _usersService = usersService;
            _jwtService = jwtService;
            _passwordEncoderService = passwordEncoderService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
        }

        public UsersModel Registration(UsersEntity user)
        {
            string encodedPassword = _passwordEncoderService.Encode(user.Password);
            user.Password = encodedPassword;
            UsersModel createdUser = UsersMapper.ToModel(_usersService.Create(user));
            TokensModel tokens = _jwtService.GenerateTokens(createdUser);
            SetAccessToken(tokens.AccessToken);
            _cookieService.Set(_refreshKey, tokens.RefreshToken);
            return createdUser;
        }

        public UsersModel Login(UsersEntity user)
        {
            UsersEntity? candidate = _usersService.GetByEmail(user.Email);
            if (candidate == null)
            {
                throw new ArgumentException($"User with ${nameof(user.Email)} == {user.Email} not founded");
            }

            bool isCorrectPassword = _passwordEncoderService.Verify(user.Password, candidate.Password);

            if (isCorrectPassword == false)
            {
                throw new ArgumentException("Password invalid");
            }

            TokensModel tokens = _jwtService.GenerateTokens(candidate);
            SetAccessToken(tokens.AccessToken);
            _cookieService.Set( _refreshKey, tokens.RefreshToken);
            UsersModel loginedUser = UsersMapper.ToModel(candidate);
            return loginedUser;
        }

        public void logout()
        {
            _cookieService.Delete(_refreshKey);
            DeleteAccessToken();
            
        }

        private void SetAccessToken(string accessToken)
        {
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext, nameof(_httpContextAccessor.HttpContext));
            _httpContextAccessor.HttpContext.Response.Headers.Add(_accessKey, $"Bearer {accessToken}");
        }

        private void DeleteAccessToken()
        {
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext, nameof(_httpContextAccessor.HttpContext));
            _httpContextAccessor.HttpContext.Response.Headers.Remove(_accessKey);
        }
    }
}
