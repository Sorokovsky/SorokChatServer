using SorokChatServer.Database.Entities;
using SorokChatServer.Models;

namespace SorokChatServer.Interfaces
{
    public interface IAuthorizationsService
    {
        public UsersModel Registration(UsersEntity user);
        public UsersModel Login(UsersEntity user);
        public void Logout();
    }
}
