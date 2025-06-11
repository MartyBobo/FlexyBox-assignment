using Application.DTOs;
using MediatR;

namespace Application.Commands;

public class CreateReviewCommand : IRequest<ReviewDto>
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}