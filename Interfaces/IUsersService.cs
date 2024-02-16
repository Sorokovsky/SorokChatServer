using SorokChatServer.Database.Entities;
using SorokChatServer.Models;

namespace SorokChatServer.Interfaces
{
    public interface IUsersService
    {
        public List<UsersModel> GetAll();
        public UsersModel GetById(long id);
        public UsersModel Create(UsersEntity user);
        public UsersModel Update(long id, UsersEntity user);
        public UsersModel Delete(long id);
    }
}
