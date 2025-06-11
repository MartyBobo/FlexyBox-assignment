using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GetRestaurantReviewsQuery : IRequest<List<ReviewDto>>
{
    public int RestaurantId { get; set; }
}

public class GetRestaurantReviewsQueryHandler : IRequestHandler<GetRestaurantReviewsQuery, List<ReviewDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRestaurantReviewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewDto>> Handle(GetRestaurantReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Where(r => r.RestaurantId == request.RestaurantId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                Username = r.User.Name,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return reviews ?? new List<ReviewDto>();
    }
}