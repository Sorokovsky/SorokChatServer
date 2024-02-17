using Microsoft.EntityFrameworkCore;
using SorokChatServer.Database.Entities;


namespace SorokChatServer.Interfaces
{
    public interface IChatContext
    {
        public DbSet<UsersEntity> Users { get; set; }
        public Task<int> SaveChangesAsync();
    }
}
