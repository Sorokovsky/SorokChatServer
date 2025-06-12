using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SorokChatServer.Application.Database.Factories;

public class ContextFactory : IDesignTimeDbContextFactory<SorokChatDatabaseContext>
{
    public SorokChatDatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return new SorokChatDatabaseContext(configuration);
    }
}