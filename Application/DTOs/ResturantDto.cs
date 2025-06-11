using Application.Helpers;
using Domain.Entities;

namespace Application.DTOs;

public class ResturantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    // Store the raw opening hours data for calculation
    internal IEnumerable<OpeningHours>? _rawOpeningHours;
    
    /// <summary>
    /// Dynamically calculated property that determines if the restaurant is currently open
    /// based on current time and opening hours for Restaurant mode
    /// </summary>
    public bool IsOpen => _rawOpeningHours != null && RestaurantHelper.CalculateIsOpen(_rawOpeningHours, "Restaurant");
    
    public Dictionary<string, List<OpeningHoursDto>> OpeningHours { get; set; } = new();
    public List<string> GalleryImages { get; set; } = new();
    
    // Review-related properties
    public decimal? AverageRating { get; set; }
    public int ReviewCount { get; set; }
}