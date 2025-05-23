// /Server/Infrastructure/Data/ApplicationDbContext.cs

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace MyApp.Server.Infrastructure.Data
{
    public class ApplicationDbContext 
        : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Resturant> Resturants    { get; set; } = null!;
        public DbSet<OpeningHours> OpeningHours  { get; set; } = null!;
        public DbSet<GalleryImage> GalleryImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Resturant>()
                .ToTable("Restaurants")
                .HasMany(r => r.OpeningHours)
                .WithOne(o => o.Resturant)
                .HasForeignKey(o => o.ResturantId);

            builder.Entity<Resturant>()
                .HasMany(r => r.GalleryImages)
                .WithOne(g => g.Resturant)
                .HasForeignKey(g => g.ResturantId);

            builder.Entity<OpeningHours>()
                .ToTable("OpeningHours");

            builder.Entity<GalleryImage>()
                .ToTable("GalleryImages");
        }
    }
}