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
            return _userRepository.GetAll();
        }

        public UsersEntity GetById(long id)
        {
            return _userRepository.GetById(id);
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
    }
}
