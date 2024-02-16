using SorokChatServer.Database.Context;
using SorokChatServer.Database.Entities;

namespace SorokChatServer.Database.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ChatContext _context;

        public UsersRepository(ChatContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UsersEntity Create(UsersEntity user)
        {
            UsersEntity created = _context.Users.Add(user).Entity;
            _context.SaveChangesAsync();
            return created;
        }

        public UsersEntity Delete(UsersEntity usersEntity)
        {
            UsersEntity deleted =  _context.Users.Remove(usersEntity).Entity;
            _context.SaveChangesAsync();
            return deleted;
        }

        public List<UsersEntity> Find(Func<UsersEntity, bool> predicate)
        {
            List<UsersEntity> users = _context.Users.Where(predicate).ToList();
            _context.SaveChangesAsync();
            return users;
        }

        public UsersEntity Update(UsersEntity user)
        {
            UsersEntity updated = _context.Users.Update(user).Entity;
            _context.SaveChangesAsync();
            return updated;
        }
    }
}
