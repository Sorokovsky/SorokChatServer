using Microsoft.EntityFrameworkCore;
using SorokChatServer.Application.Database.Entities;

namespace SorokChatServer.Application.Database;

public class SorokChatDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public SorokChatDatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql(_configuration.GetConnectionString(nameof(SorokChatDatabaseContext)))
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(SorokChatDatabaseContext).Assembly);
    }
}