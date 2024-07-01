using Authorization.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SorokChatServer.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("registration")]
        public string Registration()
        {
            _authorizationService.Registration(0);
            return "Hi";
        }
    }
}
