using SorokChatServer.Application.Database.Repositories;
using SorokChatServer.Application.Interfaces;
using SorokChatServer.Application.Services;

namespace SorokChatServer.Application.Extensions;

public static class SetupApplicationExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddSingleton<IPasswordService, BcryptPasswordService>();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddOpenApi();
    }

    public static void UseSwaggerView(this WebApplication application)
    {
        application.MapOpenApi();
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}