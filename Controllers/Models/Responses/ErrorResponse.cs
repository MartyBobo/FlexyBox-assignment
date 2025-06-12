namespace FlexyBox.Controllers.Models.Responses;

public class ErrorResponse
{
    public string Error { get; set; } = string.Empty;
    public string? Details { get; set; }
    public IDictionary<string, string[]>? ValidationErrors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ErrorResponse(string error, string? details = null)
    {
        Error = error;
        Details = details;
    }

    public ErrorResponse(string error, IDictionary<string, string[]> validationErrors)
    {
        Error = error;
        ValidationErrors = validationErrors;
    }
}