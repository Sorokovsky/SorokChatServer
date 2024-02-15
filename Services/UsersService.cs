using SorokChatServer.Database.Entities;
using SorokChatServer.Database.Repositories;
using System.Net;

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
            List<UsersEntity> users = _userRepository.Find(user => true);
            if(users.Count == 0)
            {
                throw new BadHttpRequestException("Users not found");
            }
            return users;
        }

        public UsersEntity GetById(long id)
        {
            List<UsersEntity> users = _userRepository.Find(user => user.Id == id);
            if(users.Count < 1)
            {
                throw new BadHttpRequestException($"User with id = {id} not found");
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
            return _userRepository.Delete(new UsersEntity(id));
        }
    }
}
