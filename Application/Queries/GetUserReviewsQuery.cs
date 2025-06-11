using Application.DTOs;
using MediatR;

namespace Application.Queries;

public class GetUserReviewsQuery : IRequest<List<UserReviewDto>>
{
    public int UserId { get; set; }
}