using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Data.EntityConfiguration
{
    public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users"); // Set the table name if needed

            // Primary Key
            builder.HasKey(u => u.UserId);

            // Properties
            builder.Property(u => u.Email)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$")
                .HasAnnotation("RegularExpressionErrorMessage", "Invalid email address");
            builder.Property(u => u.UserId).ValueGeneratedOnAdd();
            builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(255).IsRequired();
            builder.Property(u => u.IsAdmin).IsRequired();
        }
    }
}
