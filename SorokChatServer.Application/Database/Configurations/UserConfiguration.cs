using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorokChatServer.Application.Database.Entities;
using SorokChatServer.Application.Model;

namespace SorokChatServer.Application.Database.Configurations;

public class UserConfiguration : BaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        builder.ComplexProperty(x => x.FullName, y =>
        {
            y.Property(name => name.FirstName).HasMaxLength(FullName.MaxLength).HasDefaultValue(string.Empty);
            y.Property(name => name.LastName).HasMaxLength(FullName.MaxLength).HasDefaultValue(string.Empty);
            y.Property(name => name.MiddleName).HasMaxLength(FullName.MaxLength).HasDefaultValue(string.Empty);
        });

        builder.ComplexProperty(x => x.Email, y =>
        {
            y.Property(email => email.Value).HasColumnName("email");
        }).HasIndex().IsUnique();

        builder.ComplexProperty(x => x.Password, y =>
        {
            y.Property(password => password.Value).HasMaxLength(Password.MaxLength).IsRequired().HasColumnName("password");
        });

        builder.ComplexProperty(x => x.AvatarPath, y =>
        {
            y.Property(avatarPath => avatarPath.Value).HasColumnName("avatar_path");
        });
    }
}