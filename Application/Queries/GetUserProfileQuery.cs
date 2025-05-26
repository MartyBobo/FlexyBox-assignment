using Application.DTOs;
using MediatR;

namespace Application.Queries;

public class GetUserProfileQuery : IRequest<UserDto>
{
    public int UserId { get; set; }
    
    public GetUserProfileQuery(int userId)
    {
        UserId = userId;
    }
}
