using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;
using SorokChatServer.Exceptions;
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
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            UsersEntity created = _context.Users.First(u => u.Email == user.Email);
            return created;
        }

        public UsersEntity Delete(long id)
        {
            UsersEntity deleted = _context.Users.First(u => u.Id == id);
            _context.Users.Where(user => user.Id == id).ExecuteDelete();
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
            UsersEntity? candidate = _context.Users.First(u => u.Id == user.Id);
            if(candidate == null)
            {
                throw new NotFoundException("User not founded");
            }
            candidate.UpdatedAt = GetCurrentTime();
            if(user.Surname != null)
            {
                candidate.Surname = user.Surname;
            }
            if(user.Name != null)
            {
                candidate.Name = user.Name;
            }
            if(user.Email != null)
            {
                candidate.Email = user.Email;
            }
            if(user.AvatarPath != null)
            {
                candidate.AvatarPath = user.AvatarPath;
            }
            UsersEntity updated = _context.Users.Update(candidate).Entity;
            _context.SaveChangesAsync();
            return updated;
        }

        private static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.AddHours(-1);
        }
    }
}
