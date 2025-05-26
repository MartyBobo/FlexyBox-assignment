using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;

namespace FlexyBox.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UserService(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<UserDto> GetProfileAsync()
    {
        var query = new GetUserProfileQuery(_currentUserService.UserId);
        return await _mediator.Send(query);
    }

    public async Task UpdateProfileAsync(UserDto profile)
    {
        var command = new UpdateUserProfileCommand(_currentUserService.UserId, profile);
        await _mediator.Send(command);
    }
}
