using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, List<UserReviewDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUserReviewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserReviewDto>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .Where(r => r.UserId == request.UserId)
            .OrderByDescending(r => r.UpdatedAt)
            .Select(r => new UserReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                Username = r.User.Name,
                RestaurantId = r.RestaurantId,
                RestaurantName = r.Restaurant.Name,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return reviews;
    }
}