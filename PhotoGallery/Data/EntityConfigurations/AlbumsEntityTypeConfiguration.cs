using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Data.EntityConfigurations
{
    public class AlbumsEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            _ = builder.ToTable("Albums"); // Set the table name if needed

            // Primary Key
            _ = builder.HasKey(a => a.AlbumId);

            // Properties
            _ = builder.Property(a => a.AlbumId).ValueGeneratedOnAdd();
            _ = builder.Property(a => a.Title).HasMaxLength(255).IsRequired();
            _ = builder.Property(a => a.Description).HasMaxLength(500);

            // Relationships
            builder.HasMany(a => a.Images)
                .WithOne(i => i.Album)
                .HasForeignKey(i => i.AlbumId);
        }
    }
}
