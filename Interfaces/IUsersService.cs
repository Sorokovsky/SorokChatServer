using SorokChatServer.Database.Entities;

namespace SorokChatServer.Interfaces
{
    public interface IUsersService
    {
        public List<UsersEntity>? GetAll();
        public UsersEntity? GetById(long id);
        public UsersEntity? GetByEmail(string email);
        public UsersEntity Create(UsersEntity user);
        public UsersEntity Update(long id, UsersEntity user);
        public UsersEntity Delete(long id);
    }
}
