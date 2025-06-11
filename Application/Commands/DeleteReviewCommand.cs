using MediatR;

namespace Application.Commands;

public class DeleteReviewCommand : IRequest<bool>
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
}