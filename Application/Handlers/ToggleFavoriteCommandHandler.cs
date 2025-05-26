using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class ToggleFavoriteCommandHandler : IRequestHandler<ToggleFavoriteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ToggleFavoriteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
    {
        var existingFavorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.RestaurantId == request.RestaurantId, cancellationToken);

        if (existingFavorite != null)
        {
            // Remove from favorites
            _context.Favorites.Remove(existingFavorite);
            await _context.SaveChangesAsync(cancellationToken);
            return false; // Not favorited anymore
        }
        else
        {
            // Add to favorites
            var newFavorite = new Favorite
            {
                UserId = request.UserId,
                RestaurantId = request.RestaurantId,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.Favorites.Add(newFavorite);
            await _context.SaveChangesAsync(cancellationToken);
            return true; // Now favorited
        }
    }
}
