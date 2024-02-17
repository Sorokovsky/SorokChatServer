using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;
using SorokChatServer.Interfaces;

namespace SorokChatServer.Database.Context
{
    public class ChatContext : DbContext, IChatContext
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

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<UsersEntity> Users { get; set; }
    }
}
