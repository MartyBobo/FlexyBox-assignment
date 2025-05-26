using Application.DTOs;

namespace FlexyBox.Services;

public interface IUserService
{
    Task<UserDto> GetProfileAsync();
    Task UpdateProfileAsync(UserDto profile);
}
