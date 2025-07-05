using Microsoft.AspNetCore.Mvc;
using SorokChatServer.Infrastructure.Interfaces;

namespace SorokChatServer.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFilesService _filesService;

    public FilesController(IFilesService filesService)
    {
        _filesService = filesService;
    }

    [HttpPost("{folder}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Load(IFormFile file, [FromRoute] string folder,
        CancellationToken cancellationToken)
    {
        var path = await _filesService.LoadAsync(file, folder, Path.GetFileNameWithoutExtension(file.FileName),
            cancellationToken);
        return await Task.FromResult(Ok(path));
    }
}