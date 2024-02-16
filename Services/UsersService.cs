using SorokChatServer.Database.Entities;
using SorokChatServer.Database.Repositories;
namespace SorokChatServer.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UsersEntity> GetAll()
        {
            List<UsersEntity> users = _userRepository.Find(GetAllPredicate());
            if(users.Count == 0)
            {
                throw new Exception("Users not found");
            }
            return users;
        }

        public UsersEntity GetById(long id)
        {
            List<UsersEntity> users = _userRepository.Find(GetByIdPredicate(id));
            if(users.Count < 1)
            {
                throw new Exception($"User with id = {id} not found");
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

        private Func<UsersEntity, bool> GetByIdPredicate(long id)
        {
            return user => user.Id == id;
        }

        private Func<UsersEntity, bool> GetAllPredicate()
        {
            return user => true;
        }
    }
}
