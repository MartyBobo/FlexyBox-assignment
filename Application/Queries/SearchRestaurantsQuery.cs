using Application.DTOs;
using MediatR;

namespace Application.Queries;

public class SearchRestaurantsQuery : IRequest<List<ResturantDto>>
{
    public string SearchTerm { get; set; } = string.Empty;
}
