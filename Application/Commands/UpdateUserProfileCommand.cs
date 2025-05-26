using Application.DTOs;
using MediatR;

namespace Application.Commands;

public class UpdateUserProfileCommand : IRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    
    public UpdateUserProfileCommand(int userId, UserDto userDto)
    {
        UserId = userId;
        Name = userDto.Name;
        Email = userDto.Email;
        Phone = userDto.Phone;
        Address = userDto.Address;
    }
}
