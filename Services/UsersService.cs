using SorokChatServer.Database.Entities;
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
                return null;
            }
            return users;
        }

        public UsersEntity? GetByEmail(string email)
        {
            try
            {
                List<UsersEntity> users = _userRepository.Find(GetByEmailPredicate(email));
                if(users.Count < 1)
                {
                    return null;
                }
                return users.First();
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public UsersEntity? GetById(long id)
        {
            List<UsersEntity> entities = _userRepository.Find(GetByIdPredicate(id));
            List<UsersEntity> users = entities;
            if (users.Count < 1)
            {
                return null;
            }
            return users.First();
        }

        public UsersEntity Create(UsersEntity user)
        {
            return _userRepository.Create(user);
        }

        public UsersEntity Update(long id, UsersEntity user)
        {
            try
            {
                user.Id = id;
                return _userRepository.Update(user);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public UsersEntity Delete(long id)
        {
            try
            {
                return _userRepository.Delete(new UsersEntity(id));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
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
