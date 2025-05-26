using Application.Interfaces;
using Application.Queries;
using MediatR;

namespace Application.Handlers;

public class IsFavoriteQueryHandler : IRequestHandler<IsFavoriteQuery, bool>
{
    private readonly IApplicationDbContext _context;

    public IsFavoriteQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(IsFavoriteQuery request, CancellationToken cancellationToken)
    {
        // For now, we'll use a simple simulation
        // In a real application, you would check a favorites table in the database
        
        // TODO: Implement actual favorite check logic
        // Example:
        // var favorite = await _context.Favorites
        //     .FirstOrDefaultAsync(f => f.RestaurantId == request.RestaurantId && f.UserId == currentUserId);
        // return favorite != null;
        
        await Task.Delay(50, cancellationToken); // Simulate database operation
        
        // For demo purposes, let's simulate some restaurants being favorited
        // Restaurant IDs 1, 3, 5 will be "favorited"
        return request.RestaurantId % 2 == 1;
    }
}
