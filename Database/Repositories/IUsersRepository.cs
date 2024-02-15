using SorokChatServer.Database.Entities;

namespace SorokChatServer.Database.Repositories
{
    public interface IUsersRepository
    {
        public List<UsersEntity> Find(Func<UsersEntity, bool> exrpessuin);
        public UsersEntity Create(UsersEntity user);
        public UsersEntity Update(UsersEntity user);
        public UsersEntity Delete(UsersEntity usersEntity);
    }
}
