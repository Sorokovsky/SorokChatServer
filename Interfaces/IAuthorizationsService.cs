using SorokChatServer.Models.Users;

namespace SorokChatServer.Interfaces
{
    public interface IAuthorizationsService
    {
        public UsersModel Registration(RegistrationModel user);
        public UsersModel Login(LoginModel user);
        public void Logout();
    }
}
