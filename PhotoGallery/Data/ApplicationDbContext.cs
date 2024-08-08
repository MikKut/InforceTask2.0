using Microsoft.EntityFrameworkCore;
using PhotoGallery.Api.Host.Data.EntityConfiguration;
using PhotoGallery.Api.Host.Data.EntityConfigurations;
using PhotoGallery.Api.Models.Entities;

namespace PhotoGallery.Api.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Album> Albums { get; set; } = null!;

    public DbSet<Image> Images { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = builder.ApplyConfiguration(new ImagesEntityTypeConfiguration());
        _ = builder.ApplyConfiguration(new AlbumsEntityTypeConfiguration());
        _ = builder.ApplyConfiguration(new UsersEntityTypeConfiguration());
    }
}
