using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorokChatServer.Infrastructure.Entities;

namespace SorokChatServer.Database.Configurations;

internal abstract class BaseConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigureBaseEntity(builder);
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    private static void ConfigureBaseEntity(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(entity => entity.Id);
    }
}