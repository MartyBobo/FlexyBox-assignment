using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class SearchRestaurantsQueryHandler : IRequestHandler<SearchRestaurantsQuery, List<ResturantDto>>
{
    private readonly IApplicationDbContext _context;

    public SearchRestaurantsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResturantDto>> Handle(SearchRestaurantsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            return new List<ResturantDto>();
        }

        var searchTerm = request.SearchTerm.ToLower();

        var restaurants = await _context.Resturants
            .Include(r => r.OpeningHours)
            .Include(r => r.GalleryImages)
            .Where(r => r.Name.ToLower().Contains(searchTerm) || 
                       r.Address.ToLower().Contains(searchTerm))
            .ToListAsync(cancellationToken);

        var result = restaurants.Select(resturant => new ResturantDto
        {
            Id = resturant.Id,
            Name = resturant.Name,
            Address = resturant.Address,
            Phone = resturant.Phone,
            Email = resturant.Email,
            IsOpen = resturant.IsOpen,
            OpeningHours = resturant.OpeningHours
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
            GalleryImages = resturant.GalleryImages
                .Select(gi => gi.ImageUrl)
                .ToList()
        }).ToList();

        return result;
    }
}
