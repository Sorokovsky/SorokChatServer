using SorokChatServer.Database.Entities;
using SorokChatServer.Exceptions;
using SorokChatServer.Interfaces;
using SorokChatServer.Mappers;
using SorokChatServer.Models;

namespace SorokChatServer.Services
{
    public class AuthorizationsService : IAuthorizationsService
    {
        private readonly IUsersService _usersService;
        private readonly IJwtService _jwtService;
        private readonly IPasswordEncoderService _passwordEncoderService;
        private readonly ICookieService _cookieService;
        private readonly IBearerService _bearerService;
        private readonly string _refreshKey = "refresh_token";

        public AuthorizationsService(
            IUsersService usersService,
            IJwtService jwtService,
            IPasswordEncoderService passwordEncoderService,
            ICookieService cookieService,
            IBearerService bearerService
            )
        {
            _usersService = usersService;
            _jwtService = jwtService;
            _passwordEncoderService = passwordEncoderService;
            _cookieService = cookieService;
            _bearerService = bearerService;
        }

        public UsersModel Registration(RegistrationModel user)
        {
            try
            {
                UsersEntity candidate = _usersService.GetByEmail(user.Email);
                throw new AlreadyExistsException($"User by {nameof(user.Email)} is exists");
            }
            catch (NotFoundException)
            {
                string encodedPassword = _passwordEncoderService.Encode(user.Password);
                user.Password = encodedPassword;
                UsersModel createdUser = UsersMapper.ToModel(_usersService.Create(UsersMapper.RegistrationToEntity(user)));
                TokensModel tokens = _jwtService.GenerateTokens(createdUser);
                _bearerService.SetAccessToken(tokens.AccessToken);
                _cookieService.Set(_refreshKey, tokens.RefreshToken);
                return createdUser;
            }
        }

        public UsersModel Login(LoginModel user)
        {
            UsersEntity candidate = _usersService.GetByEmail(user.Email);
            bool isCorrectPassword = _passwordEncoderService.Verify(user.Password, candidate.Password);
            if (isCorrectPassword == false)
            {
                throw new PasswordException("Password invalid");
            }

            TokensModel tokens = _jwtService.GenerateTokens(candidate);
            _bearerService.SetAccessToken(tokens.AccessToken);
            _cookieService.Set(_refreshKey, tokens.RefreshToken);
            UsersModel loginedUser = UsersMapper.ToModel(candidate);
            return loginedUser;
        }

        public void Logout()
        {
            _cookieService.Delete(_refreshKey);
            _bearerService.DeleteAccessToken();

        }
    }
}
