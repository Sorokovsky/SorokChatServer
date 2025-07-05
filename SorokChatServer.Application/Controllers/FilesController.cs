using Microsoft.AspNetCore.Mvc;

namespace SorokChatServer.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Load(IFormFile file)
    {
        return await Task.FromResult(Ok());
    }
}