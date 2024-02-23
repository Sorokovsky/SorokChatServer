using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;
using SorokChatServer.Mappers;
using SorokChatServer.Models.Users;

namespace SorokChatServer.Controllers
{
    [ApiController, Route("authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationsService _authorizationService;

        public AuthorizationController(IAuthorizationsService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("registration")]
        public ActionResult Registration([FromBody] RegistrationModel user)
        {
            try
            {
                UsersModel createdUser = _authorizationService.Registration(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            return Ok(_authorizationService.Login(model));
        }

        [HttpDelete("logout"), Authorize]
        public ActionResult Logout()
        {
            _authorizationService.Logout();
            return Ok();
        }
    }
}
