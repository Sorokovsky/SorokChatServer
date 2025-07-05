using SorokChatServer.Core.Services;
using SorokChatServer.Database;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Infrastructure.Interfaces;
using SorokChatServer.Infrastructure.Mapping;

namespace SorokChatServer.Application.Extensions;

public static class StartupExtensions
{
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UsersProfile));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IFilesService, FilesService>();
    }
}