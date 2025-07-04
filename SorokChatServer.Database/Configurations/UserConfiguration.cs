using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorokChatServer.Infrastructure.Entities;

namespace SorokChatServer.Database.Configurations;

internal class UserConfiguration : BaseConfigurations<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
    }
}