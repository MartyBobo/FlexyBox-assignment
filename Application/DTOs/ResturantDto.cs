namespace Application.DTOs;

public class ResturantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOpen { get; set; }
    
    public Dictionary<string, List<OpeningHoursDto>> OpeningHours { get; set; } = new();
    public List<string> GalleryImages { get; set; } = new();
}