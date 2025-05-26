using MediatR;

namespace Application.Queries;

public class IsFavoriteQuery : IRequest<bool>
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
}
