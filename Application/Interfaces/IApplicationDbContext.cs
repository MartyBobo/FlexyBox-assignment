using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Resturant> Resturants { get; }
    DbSet<OpeningHours> OpeningHours { get; }
    DbSet<GalleryImage> GalleryImages { get; }
    DbSet<Favorite> Favorites { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}