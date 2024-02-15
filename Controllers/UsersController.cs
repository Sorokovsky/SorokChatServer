using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SorokChatServer.Database.Entities;
using SorokChatServer.Services;

namespace SorokChatServer.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public List<UsersEntity> GetAll()
        {
            return _usersService.GetAll();
        }

        [HttpGet("{id}")]
        public UsersEntity GetById(long id)
        {
            return _usersService.GetById(id);
        }

        [HttpPost]
        public UsersEntity Create([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] UsersEntity entity)
        {
            return _usersService.Create(entity);
        }
    }
}
