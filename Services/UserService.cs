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

    public async Task<List<UserReviewDto>> GetUserReviewsAsync()
    {
        var query = new GetUserReviewsQuery { UserId = _currentUserService.UserId };
        return await _mediator.Send(query);
    }

    public async Task<ReviewDto> UpdateReviewAsync(int reviewId, int rating, string comment)
    {
        var command = new UpdateReviewCommand
        {
            ReviewId = reviewId,
            UserId = _currentUserService.UserId,
            Rating = rating,
            Comment = comment
        };
        
        try
        {
            return await _mediator.Send(command);
        }
        catch (InvalidOperationException)
        {
            // Re-throw to preserve the specific error message
            throw;
        }
    }

    public async Task<bool> DeleteReviewAsync(int reviewId)
    {
        var command = new DeleteReviewCommand
        {
            ReviewId = reviewId,
            UserId = _currentUserService.UserId
        };
        
        try
        {
            return await _mediator.Send(command);
        }
        catch (InvalidOperationException)
        {
            // Re-throw to preserve the specific error message
            throw;
        }
    }
}
