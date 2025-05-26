using Application.DTOs;
using MediatR;

namespace Application.Queries;

public class GetFavoritesByUserQuery : IRequest<List<ResturantDto>>
{
    public int UserId { get; set; }
}
