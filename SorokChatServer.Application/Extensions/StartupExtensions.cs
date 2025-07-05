using Microsoft.OpenApi.Models;
using SorokChatServer.Core.Services;
using SorokChatServer.Database;
using SorokChatServer.Database.Repositories;
using SorokChatServer.Infrastructure.Filters;
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

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            c.OperationFilter<FileUploadOperationFilter>();

            c.MapType<IFormFile>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });

            c.AddSecurityDefinition("multipart/form-data", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
        });
    }
}