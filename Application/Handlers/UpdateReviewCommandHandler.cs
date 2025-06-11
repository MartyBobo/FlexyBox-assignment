using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
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

        // Find the review and ensure it belongs to the user
        var review = await _context.Reviews
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId && r.UserId == request.UserId, cancellationToken);

        if (review == null)
        {
            throw new InvalidOperationException("Review not found or you don't have permission to edit it");
        }

        // Update the review
        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            Username = review.User.Name,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt
        };
    }
}