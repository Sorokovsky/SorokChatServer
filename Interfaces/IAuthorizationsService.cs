using SorokChatServer.Database.Entities;
using SorokChatServer.Models;

namespace SorokChatServer.Interfaces
{
    public interface IAuthorizationsService
    {
        public UsersModel Registration(UsersEntity user);
        public UsersModel Login(LoginModel user);
        public void Logout();
    }
}
