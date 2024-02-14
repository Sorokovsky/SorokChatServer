using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;

namespace SorokChatServer.Database.Context
{
    public class ChatContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ChatContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
    }

        public DbSet<UsersEntity> Users { get; set; }
    }
}
