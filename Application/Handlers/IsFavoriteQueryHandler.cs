using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.RestaurantId == request.RestaurantId, cancellationToken);
        
        return favorite != null;
    }
}
