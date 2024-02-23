using SorokChatServer.Database.Entities;

namespace SorokChatServer.Interfaces
{
    public interface IUsersRepository
    {
        public List<UsersEntity> Find(Func<UsersEntity, bool> exrpessuin);
        public UsersEntity Create(UsersEntity user);
        public UsersEntity Update(UsersEntity user);
        public UsersEntity Delete(long id);
    }
}
