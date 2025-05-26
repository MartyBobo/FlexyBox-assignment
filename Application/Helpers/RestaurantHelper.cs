using Domain.Entities;

namespace Application.Helpers;

public static class RestaurantHelper
{
    /// <summary>
    /// Calculates if a restaurant is currently open based on its opening hours and current time
    /// </summary>
    /// <param name="openingHours">List of opening hours for the restaurant</param>
    /// <param name="mode">Operating mode to check (defaults to "Restaurant")</param>
    /// <param name="currentDateTime">Current date/time (optional, defaults to DateTime.Now for testing)</param>
    /// <returns>True if the restaurant is currently open, false otherwise</returns>
    public static bool CalculateIsOpen(IEnumerable<OpeningHours> openingHours, string mode = "Restaurant", DateTime? currentDateTime = null)
    {
        var now = currentDateTime ?? DateTime.Now;
        var currentDay = now.DayOfWeek.ToString();
        var currentTime = now.TimeOfDay;

        // Find opening hours for the current day and mode
        var todaysHours = openingHours.FirstOrDefault(oh => 
            oh.Mode.Equals(mode, StringComparison.OrdinalIgnoreCase) && 
            oh.Day.Equals(currentDay, StringComparison.OrdinalIgnoreCase));

        // If no opening hours found for today in this mode, restaurant is closed
        if (todaysHours == null)
        {
            return false;
        }

        // If StartTime or EndTime is null, restaurant is closed
        if (!todaysHours.StartTime.HasValue || !todaysHours.EndTime.HasValue)
        {
            return false;
        }

        var startTime = todaysHours.StartTime.Value;
        var endTime = todaysHours.EndTime.Value;

        // Handle normal case where restaurant closes on the same day
        if (startTime <= endTime)
        {
            return currentTime >= startTime && currentTime <= endTime;
        }
        
        // Handle case where restaurant closes after midnight (endTime < startTime)
        // Restaurant is open if current time is after start OR before end
        return currentTime >= startTime || currentTime <= endTime;
    }
}
