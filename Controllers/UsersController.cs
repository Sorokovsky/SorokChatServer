using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SorokChatServer.Database.Entities;
using SorokChatServer.Services;

namespace SorokChatServer.Controllers
{
    [ApiController, Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult<List<UsersEntity>> GetAll()
        {
            try 
            {
                List<UsersEntity> users = _usersService.GetAll();
                return Ok(users);
            } 
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UsersEntity> GetById(long id)
        {
            try 
            {
                UsersEntity user = _usersService.GetById(id);
                return Ok(user);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public UsersEntity Create([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] UsersEntity entity)
        {
            return _usersService.Create(entity);
        }
    }
}
