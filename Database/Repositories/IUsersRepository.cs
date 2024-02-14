using SorokChatServer.Database.Entities;

namespace SorokChatServer.Database.Repositories
{
    public interface IUsersRepository
    {
        public UsersEntity GetById(long id);
        public List<UsersEntity> GetAll();
        public UsersEntity Create(UsersEntity user);
        public UsersEntity Update(UsersEntity user);
        public UsersEntity Delete(long id);
    }
}
