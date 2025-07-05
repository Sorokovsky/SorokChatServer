using Microsoft.AspNetCore.Http;

namespace SorokChatServer.Infrastructure.Interfaces;

public interface IFilesService
{
    public Task LoadAsync(IFormFile file, string folder, string fileName, CancellationToken cancellationToken);

    public Task DeleteAsync(string path, CancellationToken cancellationToken);
}