using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [StringLength(255, ErrorMessage = "Email must be less than 255 characters")]
    public string Email { get; set; } = string.Empty;
    
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    [StringLength(20, ErrorMessage = "Phone number must be less than 20 characters")]
    public string? Phone { get; set; }
    
    [StringLength(255, ErrorMessage = "Address must be less than 255 characters")]
    public string? Address { get; set; }
}
