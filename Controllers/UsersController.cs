using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SorokChatServer.Database.Entities;
using SorokChatServer.Models;
using SorokChatServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SorokChatServer.Mappers;

namespace SorokChatServer.Controllers
{
    [ApiController, Route("users"), Authorize]
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
            List<UsersEntity>? entities = _usersService.GetAll();
            if (entities == null)
            {
                throw new Exception("Users not founded");
            }
            List<UsersModel> users = UsersMapper.ToModels(entities);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UsersModel> GetById(long id)
        {
            UsersEntity? entity = _usersService.GetById(id);
            if (entity == null)
            {
                throw new Exception($"User with {nameof(id)} == {id} not founded");
            }
            UsersModel user = UsersMapper.ToModel(entity);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UsersModel> Create([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] UsersEntity entity)
        {
            UsersModel user = UsersMapper.ToModel(_usersService.Create(entity));
            return Created("/users", user);
        }

        [HttpPut("{id}")]
        public ActionResult<UsersModel> Update(long id, [FromBody] UsersEntity entity)
        {
            UsersModel user = UsersMapper.ToModel(_usersService.Update(id, entity));
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public ActionResult<UsersModel> Delete(long id)
        {
            UsersModel user = UsersMapper.ToModel(_usersService.Delete(id));
            return Ok(user);
        }
    }
}
