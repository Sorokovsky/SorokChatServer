using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorokChatServer.Application.Database.Entities;

namespace SorokChatServer.Application.Database.Configurations;

public class UserConfiguration : BaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}