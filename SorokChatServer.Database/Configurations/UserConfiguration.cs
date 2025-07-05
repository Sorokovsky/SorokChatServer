using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorokChatServer.Infrastructure.Entities;
using SorokChatServer.Infrastructure.ValueObjects;

namespace SorokChatServer.Database.Configurations;

internal class UserConfiguration : BaseConfigurations<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.OwnsOne(user => user.Email, email =>
        {
            email.Property(x => x.Value).HasColumnName(nameof(email)).IsRequired().HasMaxLength(Email.MaxLength);
            email.HasIndex(x => x.Value).IsUnique();
        });

        builder.OwnsOne(user => user.Name, name =>
        {
            name.Property(x => x.First).HasColumnName("first_name").HasMaxLength(Name.MaxFirstNameLength);
            name.Property(x => x.Last).HasColumnName("last_name").HasMaxLength(Name.MaxLastNameLength);
            name.Property(x => x.Middle).HasColumnName("middle_name").HasMaxLength(Name.MaxMiddleNameLength);
        });

        builder.OwnsOne(user => user.Password,
            password =>
            {
                password.Property(x => x.Value).HasColumnName(nameof(password)).IsRequired()
                    .HasMaxLength(Password.MaxLength);
            });
    }
}