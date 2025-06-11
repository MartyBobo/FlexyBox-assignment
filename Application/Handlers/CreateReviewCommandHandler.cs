using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        // Validate rating
        if (request.Rating < 1 || request.Rating > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5 stars");
        }

        // Validate comment
        if (string.IsNullOrWhiteSpace(request.Comment))
        {
            throw new ArgumentException("Comment is required");
        }

        if (request.Comment.Length > 1000)
        {
            throw new ArgumentException("Comment cannot exceed 1000 characters");
        }

        // Check if user already has a review for this restaurant
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == request.UserId && 
                                     r.RestaurantId == request.RestaurantId, 
                                     cancellationToken);

        if (existingReview != null)
        {
            throw new InvalidOperationException("You have already reviewed this restaurant");
        }

        // Create new review
        var review = new Review
        {
            UserId = request.UserId,
            RestaurantId = request.RestaurantId,
            Rating = request.Rating,
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        // Get the user information for the response
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            Username = user?.Name ?? "Unknown User",
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt
        };
    }
}