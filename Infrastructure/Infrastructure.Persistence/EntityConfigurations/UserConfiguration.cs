using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(u => u.Id).HasColumnName("user_id");
            builder.Property(u => u.Email).HasColumnName("email");
            builder.Property(u => u.FirstName).HasColumnName("first_name");
            builder.Property(u => u.LastName).HasColumnName("last_name");
            builder.Property(u => u.Patronymic).HasColumnName("patronymic");
            builder.Property(u => u.PhoneNumber).HasColumnName("phone_number");
            builder.Property(u => u.Birthday).HasColumnName("birthday");
            builder.Property(u => u.Sex).HasColumnName("sex");
            builder.Property(u => u.Role).HasColumnName("role");
            builder.Property(u => u.IsBanned).HasColumnName("is_banned");
        }
    }
}
