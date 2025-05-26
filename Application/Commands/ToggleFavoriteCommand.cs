using MediatR;

namespace Application.Commands;

public class ToggleFavoriteCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
}
