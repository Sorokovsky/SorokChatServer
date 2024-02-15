using Microsoft.EntityFrameworkCore;
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
            _context.SaveChanges();
            return created;
        }

        public UsersEntity Delete(long id)
        {
            UsersEntity deleted =  _context.Users.Remove(new UsersEntity() { Id = id }).Entity;
            _context.SaveChanges();
            return deleted;
        }

        public List<UsersEntity> GetAll()
        {
            List<UsersEntity> users = _context.Users.FromSqlRaw("SELECT * FROM users").ToList();
            _context.SaveChanges();
            return users;

        }

        public UsersEntity GetById(long id)
        {
            try
            {
                UsersEntity user = _context.Users.First(user => user.Id == id);
                _context.SaveChanges();
                return user;
            } catch(Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        public UsersEntity Update(UsersEntity user)
        {
            UsersEntity updated = _context.Users.Update(user).Entity;
            _context.SaveChanges();
            return updated;
        }
    }
}
