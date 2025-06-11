using Application.DTOs;
using Application.Resturants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexyBox.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResturantDto>> GetRestaurant(int id)
    {
        try
        {
            var query = new GetResturantByIdQuery { Id = id };
            var restaurant = await _mediator.Send(query);
            
            if (restaurant == null)
            {
                return NotFound();
            }
            
            return Ok(restaurant);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving the restaurant.");
        }
    }

    [HttpPost("{id}/favorite")]
    public Task<IActionResult> ToggleFavorite(int id)
    {
        try
        {
            // For now, just return success - you can implement actual favorite logic later
            // TODO: Implement ToggleFavoriteCommand when you create the favorites feature
            return Task.FromResult<IActionResult>(Ok(new { Message = "Favorite toggled successfully" }));
        }
        catch (Exception)
        {
            return Task.FromResult<IActionResult>(StatusCode(500, "An error occurred while toggling favorite."));
        }
    }

    [HttpGet("{id}/favorite")]
    public Task<ActionResult<bool>> IsFavorite(int id)
    {
        try
        {
            // For now, return false - implement actual logic later
            // TODO: Implement GetIsFavoriteQuery when you create the favorites feature
            return Task.FromResult<ActionResult<bool>>(Ok(false));
        }
        catch (Exception)
        {
            return Task.FromResult<ActionResult<bool>>(StatusCode(500, "An error occurred while checking favorite status."));
        }
    }
}
