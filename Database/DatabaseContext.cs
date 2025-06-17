using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql(_configuration.GetConnectionString(nameof(DatabaseContext)))
            .UseSnakeCaseNamingConvention();
    }
}