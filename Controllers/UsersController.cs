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
            catch (Exception exception)
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
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult<UsersEntity> Create([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] UsersEntity entity)
        {
            UsersEntity user = _usersService.Create(entity);
            return Created("/users", user);
        }

        [HttpPut("{id}")]
        public ActionResult<UsersEntity> Update(long id, [FromBody] UsersEntity entity)
        {
            try
            {
                UsersEntity user = _usersService.Update(id, entity);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<UsersEntity> Delete(long id)
        {
            try
            {
                UsersEntity user = _usersService.Delete(id);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
