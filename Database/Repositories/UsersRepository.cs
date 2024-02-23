using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;

namespace SorokChatServer.Database.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IChatContext _context;

        public UsersRepository(IChatContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UsersEntity Create(UsersEntity user)
        {
            user.CreatedAt = DateTime.UtcNow.AddHours(-1);
            user.UpdatedAt = DateTime.UtcNow.AddHours(-1);
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
            user.UpdatedAt = DateTime.UtcNow.AddHours(-1);
            UsersEntity updated = _context.Users.Update(user).Entity;
            _context.SaveChangesAsync();
            return updated;
        }
    }
}
