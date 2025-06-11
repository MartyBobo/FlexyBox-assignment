namespace Domain.Entities;

public class Resturant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOpen { get; set; }
    
    public ICollection<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();
    public ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}