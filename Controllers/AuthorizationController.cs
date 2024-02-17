using Microsoft.AspNetCore.Mvc;
using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;
using SorokChatServer.Models;

namespace SorokChatServer.Controllers
{
    [ApiController, Route("/authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public ActionResult Registration([FromBody] UsersEntity user)
        {
            UsersModel createdUser = _authorizationService.Registration(user);
            return Ok(createdUser);
        }
    }
}
