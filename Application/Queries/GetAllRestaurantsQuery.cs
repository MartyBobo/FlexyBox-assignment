using Application.DTOs;
using MediatR;

namespace Application.Queries;

public class GetAllRestaurantsQuery : IRequest<List<ResturantDto>>
{
}
