using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int RestaurantId { get; set; }
    
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
    public int Rating { get; set; }
    
    [Required]
    [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
    public string Comment { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Resturant Restaurant { get; set; } = null!;
}