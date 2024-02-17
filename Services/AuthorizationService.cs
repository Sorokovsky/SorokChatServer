using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;
using SorokChatServer.Models;

namespace SorokChatServer.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersService _usersService;
        private readonly IJwtService _jwtService;
        private readonly IPasswordEncoderService _passwordEncoderService;

        public AuthorizationService(IUsersService usersService, IJwtService jwtService, IPasswordEncoderService passwordEncoderService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
            _passwordEncoderService = passwordEncoderService;
        }

        public UsersModel Registration(UsersEntity user)
        {
            string encodedPassword = _passwordEncoderService.Encode(user.Password);
            user.Password = encodedPassword;
            UsersModel createdUser = _usersService.Create(user);
            TokensModel tokens = _jwtService.GenerateTokens(createdUser);
            return createdUser;
        }

        public UsersModel Login(UsersEntity user)
        {
            throw new NotImplementedException();
        }

        public void logout()
        {
            throw new NotImplementedException();
        }
    }
}
