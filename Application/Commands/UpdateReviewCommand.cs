using Application.DTOs;
using MediatR;

namespace Application.Commands;

public class UpdateReviewCommand : IRequest<ReviewDto>
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}