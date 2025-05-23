namespace Domain.Entities;

public class OpeningHours
{
    public int Id { get; set; }
    public int ResturantId { get; set; }
    public Resturant Resturant { get; set; } = null!;
    public string Mode { get; set; } = string.Empty;

    public string Day { get; set; } = string.Empty;
    
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}