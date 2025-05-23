using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Resturants.Queries
{
    public class GetResturantByIdQuery : IRequest<ResturantDto>
    {
        public int Id { get; set; }

        public class GetResturantByIdQueryHandler : IRequestHandler<GetResturantByIdQuery, ResturantDto>
        {
            private readonly IApplicationDbContext _context;

            public GetResturantByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResturantDto> Handle(GetResturantByIdQuery request, CancellationToken cancellationToken)
            {
                var resturant = await _context.Resturants
                    .Include(r => r.OpeningHours)
                    .Include(r => r.GalleryImages)
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                if (resturant == null)
                {
                    return null;
                }

                var resturantDto = new ResturantDto
                {
                    Id = resturant.Id,
                    Name = resturant.Name,
                    Address = resturant.Address,
                    Phone = resturant.Phone,
                    Email = resturant.Email,
                    IsOpen = resturant.IsOpen
                };

                resturantDto.OpeningHours = resturant.OpeningHours
                    .GroupBy(oh => oh.Mode)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(oh => new OpeningHoursDto
                        {
                            Day = oh.Day,
                            Time = oh.StartTime.HasValue
                                ? $"{oh.StartTime:hh\\:mm} – {oh.EndTime:hh\\:mm}"
                                : "Closed"
                        }).ToList());

                // Map gallery images to URLs
                resturantDto.GalleryImages = resturant.GalleryImages
                    .Select(gi => gi.ImageUrl)
                    .ToList();

                return resturantDto;
            }
        }
    }
}