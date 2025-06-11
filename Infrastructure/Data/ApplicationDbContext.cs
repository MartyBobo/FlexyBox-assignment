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
        public DbSet<Favorite> Favorites { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

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
                .ToTable("OpeningHours");            builder.Entity<GalleryImage>()
                .ToTable("GalleryImages");

            builder.Entity<Favorite>()
                .ToTable("Favorites")
                .HasKey(f => f.Id);

            builder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.RestaurantId })
                .IsUnique();

            builder.Entity<Favorite>()
                .HasOne(f => f.Restaurant)
                .WithMany()
                .HasForeignKey(f => f.RestaurantId);

            // User entity configuration
            builder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            builder.Entity<User>()
                .HasMany(u => u.Favorites)
                .WithOne()
                .HasForeignKey(f => f.UserId);

            builder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Review entity configuration
            builder.Entity<Review>()
                .ToTable("Reviews")
                .HasKey(r => r.Id);

            builder.Entity<Review>()
                .Property(r => r.Rating)
                .IsRequired();

            builder.Entity<Review>()
                .Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            // Unique constraint: one review per user per restaurant
            builder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.RestaurantId })
                .IsUnique();

            // Review relationships
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(res => res.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}