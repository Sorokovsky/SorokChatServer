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
    }
}
