using MediatR;

namespace Application.Queries;

public class IsFavoriteQuery : IRequest<bool>
{
    public int RestaurantId { get; set; }
}
