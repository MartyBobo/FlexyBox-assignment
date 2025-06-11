using Application.Commands;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        // Find the review and ensure it belongs to the user
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId && r.UserId == request.UserId, cancellationToken);

        if (review == null)
        {
            throw new InvalidOperationException("Review not found or you don't have permission to delete it");
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}