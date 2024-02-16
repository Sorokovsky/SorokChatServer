using SorokChatServer.Database.Entities;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Mappers;
using SorokChatServer.Models;

namespace SorokChatServer.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UsersModel> GetAll()
        {
            List<UsersModel> users = UsersMapper.ToModels(_userRepository.Find(GetAllPredicate()));
            if(users.Count == 0)
            {
                throw new Exception("Users not found");
            }
            return users;
        }

        public UsersModel GetById(long id)
        {
            List<UsersEntity> entities = _userRepository.Find(GetByIdPredicate(id));
            List<UsersModel> users = UsersMapper.ToModels(entities);
            if(users.Count < 1)
            {
                throw new Exception($"User with id = {nameof(id)} not found");
            }
            return users.First();
        }

        public UsersModel Create(UsersEntity user)
        {
            return UsersMapper.ToModel(_userRepository.Create(user));
        }

        public UsersModel Update(long id, UsersEntity user)
        {   
            try
            {
                user.Id = id;
                return UsersMapper.ToModel(_userRepository.Update(user));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public UsersModel Delete(long id)
        {
            try 
            {
                return UsersMapper.ToModel(_userRepository.Delete(new UsersEntity(id)));
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
    }
}
