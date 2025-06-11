using Application.DTOs;

namespace FlexyBox.Services;

public interface IUserService
{
    Task<UserDto> GetProfileAsync();
    Task UpdateProfileAsync(UserDto profile);
    Task<List<UserReviewDto>> GetUserReviewsAsync();
    Task<ReviewDto> UpdateReviewAsync(int reviewId, int rating, string comment);
    Task<bool> DeleteReviewAsync(int reviewId);
}
