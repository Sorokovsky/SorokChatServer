using Microsoft.AspNetCore.Http;
using SorokChatServer.Infrastructure.Interfaces;

namespace SorokChatServer.Core.Services;

public class FilesService : IFilesService
{
    private const string Files = nameof(Files);

    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), Files);

    public FilesService()
    {
        if (Directory.Exists(_basePath) is false) Directory.CreateDirectory(_basePath);
    }

    public async Task LoadAsync(IFormFile file, string folder, string fileName, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(file.FileName);
        var fullFileName = $"{fileName}.{extension}";
        var path = Path.Combine(_basePath, folder, fullFileName);
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folder);
        if (Directory.Exists(folderPath) is false) Directory.CreateDirectory(folderPath);

        await using var fileStream = File.Open(path, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);
    }

    public Task DeleteAsync(string path, CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(_basePath, path);
        if (Directory.Exists(fullPath)) Directory.Delete(fullPath, true);
        if (File.Exists(fullPath)) File.Delete(fullPath);
        return Task.CompletedTask;
    }
}