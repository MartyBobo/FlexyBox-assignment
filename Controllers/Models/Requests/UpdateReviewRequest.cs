using System.ComponentModel.DataAnnotations;

namespace FlexyBox.Controllers.Models.Requests;

public class UpdateReviewRequest
{
    [Required(ErrorMessage = "Rating is required")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }
    
    [Required(ErrorMessage = "Comment is required")]
    [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
    public string Comment { get; set; } = string.Empty;
}