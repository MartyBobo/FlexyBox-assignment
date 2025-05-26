namespace Domain.Entities;

public class Favorite
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public Resturant Restaurant { get; set; } = null!;
}
