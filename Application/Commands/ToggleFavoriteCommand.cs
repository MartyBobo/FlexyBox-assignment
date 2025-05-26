using MediatR;

namespace Application.Commands;

public class ToggleFavoriteCommand : IRequest<bool>
{
    public int RestaurantId { get; set; }
}
