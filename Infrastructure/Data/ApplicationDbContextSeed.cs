// Infrastructure/Data/ApplicationDbContextSeed.cs

using Domain.Entities;
using MyApp.Server.Infrastructure.Data;

public class DataSeeder
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Only seed if no restaurants exist
        if (!context.Resturants.Any())
        {
            var restaurant = new Resturant
            {
                Name = "Aalborg Restaurant",
                Address = "østerågade 27, 9000 Aalborg",
                Phone = "+45 11 22 33 44",
                Email = "aalborg@flexybox.com",
                IsOpen = true
            };
            
            context.Resturants.Add(restaurant);
            await context.SaveChangesAsync();
            
            // Add opening hours
            // Add opening hours for Monday
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Monday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            
            // Add opening hours for Tuesday
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Tuesday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            
            // Add opening hours for Wednesday
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Wednesday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            
            // Add opening hours for Thursday
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Thursday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Friday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Saturday", 
                StartTime = new TimeSpan(7, 0, 0),
                EndTime = new TimeSpan(23, 0, 0)
            });
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Sunday", 
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            context.OpeningHours.Add(new OpeningHours 
            { 
                ResturantId = restaurant.Id,
                Mode = "Restaurant",
                Day = "Holiday"
            });
            // Add opening hours for Takeaway mode
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Monday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Tuesday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Wednesday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Thursday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Friday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(21, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Saturday",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Sunday",
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(20, 0, 0)
            });
            
            
            // Add opening hours for Buffet mode
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Monday",
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(14, 30, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Tuesday",
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(14, 30, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Wednesday",
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(14, 30, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Thursday",
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(14, 30, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Friday",
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(14, 30, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Saturday",
                StartTime = new TimeSpan(12, 0, 0),
                EndTime = new TimeSpan(15, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Sunday",
                StartTime = new TimeSpan(12, 0, 0),
                EndTime = new TimeSpan(15, 0, 0)
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Buffet",
                Day = "Holiday"
            });
            
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Takeaway",
                Day = "Holiday"
            });
            
            // Add opening hours for Special Events mode
            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Monday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 0, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Tuesday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 0, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Wednesday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 0, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Thursday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 0, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Friday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 59, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Saturday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 59, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Sunday",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(22, 0, 0)
            });

            context.OpeningHours.Add(new OpeningHours
            {
                ResturantId = restaurant.Id,
                Mode = "Special Events",
                Day = "Holiday"
            });            
            
            
            //add images for gallery
            context.GalleryImages.Add(new GalleryImage
            {
                ResturantId = restaurant.Id,
                ImageUrl = "/images/gallery/one.jpg"
            });

            context.GalleryImages.Add(new GalleryImage
            {
                ResturantId = restaurant.Id,
                ImageUrl = "/images/gallery/two.jpg"
            });

            context.GalleryImages.Add(new GalleryImage
            {
                ResturantId = restaurant.Id,
                ImageUrl = "/images/gallery/three.jpg"
            });
            await context.SaveChangesAsync();
        }
    }
}