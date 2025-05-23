namespace Domain.Entities;

public class GalleryImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int ResturantId { get; set; }
    public Resturant Resturant { get; set; } = null!;
}