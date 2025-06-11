namespace Application.DTOs;

public class UserReviewDto : ReviewDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
}