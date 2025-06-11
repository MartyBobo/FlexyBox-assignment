using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;

public class GetFavoritesByUserQueryHandler : IRequestHandler<GetFavoritesByUserQuery, List<ResturantDto>>
{
    private readonly IApplicationDbContext _context;

    public GetFavoritesByUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResturantDto>> Handle(GetFavoritesByUserQuery request, CancellationToken cancellationToken)
    {
        var favorites = await _context.Favorites
            .Where(f => f.UserId == request.UserId)
            .Include(f => f.Restaurant)
                .ThenInclude(r => r.OpeningHours)
            .Include(f => f.Restaurant)
                .ThenInclude(r => r.GalleryImages)
            .Include(f => f.Restaurant)
                .ThenInclude(r => r.Reviews)
            .Select(f => f.Restaurant)
            .ToListAsync(cancellationToken);        var result = favorites.Select(restaurant => new ResturantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            Phone = restaurant.Phone,
            Email = restaurant.Email,
            _rawOpeningHours = restaurant.OpeningHours,
            OpeningHours = restaurant.OpeningHours
                .GroupBy(oh => oh.Mode)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(oh => new OpeningHoursDto
                    {
                        Day = oh.Day,
                        Time = oh.StartTime.HasValue
                            ? $"{oh.StartTime:hh\\:mm} – {oh.EndTime:hh\\:mm}"
                            : "Closed"
                    }).ToList()),
            GalleryImages = restaurant.GalleryImages
                .Select(gi => gi.ImageUrl)
                .ToList(),
            AverageRating = restaurant.Reviews.Any() 
                ? (decimal?)Math.Round(restaurant.Reviews.Average(r => r.Rating), 1) 
                : null,
            ReviewCount = restaurant.Reviews.Count
        }).ToList();

        return result;
    }
}
