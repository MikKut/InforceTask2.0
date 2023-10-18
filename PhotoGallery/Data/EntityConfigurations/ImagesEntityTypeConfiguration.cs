using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoGallery.Api.Models.Entities;


namespace PhotoGallery.Api.Host.Data.EntityConfigurations
{
    public class ImagesEntityTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            // Primary Key
            builder.HasKey(i => i.ImageId);

            // Properties
            builder.Property(i => i.ImageId).ValueGeneratedOnAdd();
            builder.Property(i => i.Title).HasMaxLength(255).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(500);
            builder.Property(i => i.Content).IsRequired();
            builder.Property(i => i.Likes).IsRequired();
            builder.Property(i => i.Dislikes).IsRequired();

            // Relationships
            builder.HasOne(i => i.Album)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
