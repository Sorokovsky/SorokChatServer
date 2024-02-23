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
            user.CreatedAt = GetCurrentTime();
            user.UpdatedAt = GetCurrentTime();
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
            user.UpdatedAt = GetCurrentTime();
            UsersEntity updated = _context.Users.Update(user).Entity;
            _context.SaveChangesAsync();
            return updated;
        }

        private DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.AddHours(-1);
        }
    }
}
