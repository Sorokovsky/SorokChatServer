using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SorokChatServer.Database.Entities;

namespace SorokChatServer.Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString(nameof(DatabaseContext)))
            .UseSnakeCaseNamingConvention();
    }
}