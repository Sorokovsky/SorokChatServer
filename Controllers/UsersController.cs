using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SorokChatServer.Database.Entities;
using SorokChatServer.Models;
using SorokChatServer.Interfaces;

namespace SorokChatServer.Controllers
{
    [ApiController, Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult<List<UsersModel>> GetAll()
        {
            try 
            {
                List<UsersModel> users = _usersService.GetAll();
                return Ok(users);
            } 
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UsersModel> GetById(long id)
        {
            try 
            {
                UsersModel user = _usersService.GetById(id);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult<UsersModel> Create([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] UsersEntity entity)
        {
            UsersModel user = _usersService.Create(entity);
            return Created("/users", user);
        }

        [HttpPut("{id}")]
        public ActionResult<UsersModel> Update(long id, [FromBody] UsersEntity entity)
        {
            try
            {
                UsersModel user = _usersService.Update(id, entity);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<UsersModel> Delete(long id)
        {
            try
            {
                UsersModel user = _usersService.Delete(id);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
