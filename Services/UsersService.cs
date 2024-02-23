using SorokChatServer.Database.Entities;
using SorokChatServer.Exceptions;
using SorokChatServer.Interfaces;

namespace SorokChatServer.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UsersEntity>? GetAll()
        {
            List<UsersEntity> users = _userRepository.Find(GetAllPredicate());
            if (users.Count == 0)
            {
               throw new NotFoundException("Users not found");
            }
            return users;
        }

        public UsersEntity GetByEmail(string email)
        {
            List<UsersEntity> users = _userRepository.Find(GetByEmailPredicate(email));
            if(users.Count < 1)
            {
                throw new NotFoundException($"User by {nameof(email)} not found");
            }
            return users.First();
        }

        public UsersEntity GetById(long id)
        {
            List<UsersEntity> users = _userRepository.Find(GetByIdPredicate(id));
            if (users.Count < 1)
            {
                throw new NotFoundException($"User by {nameof(id)} not found");
            }
            return users.First();
        }

        public UsersEntity Create(UsersEntity user)
        {
            return _userRepository.Create(user);
        }

        public UsersEntity Update(long id, UsersEntity user)
        {
            user.Id = id;
            return _userRepository.Update(user);
        }

        public UsersEntity Delete(long id)
        {
            return _userRepository.Delete(id);
        }

        private static Func<UsersEntity, bool> GetByIdPredicate(long id)
        {
            return user => user.Id == id;
        }

        private static Func<UsersEntity, bool> GetAllPredicate()
        {
            return user => true;
        }

        private static Func<UsersEntity, bool> GetByEmailPredicate(string email)
        {
            return user => user.Email == email;
        }
    }
}
