using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;


namespace SorokChatServer.Interfaces
{
    public interface IChatContext : IDisposable
    {
        public DbSet<UsersEntity> Users { get; set; }
        public Task<int> SaveChangesAsync();
        protected void OnConfiguring(DbContextOptionsBuilder options);
    }
}
