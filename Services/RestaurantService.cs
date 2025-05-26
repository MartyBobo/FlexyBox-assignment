using Application.DTOs;
using Application.Resturants.Queries;
using MediatR;

namespace FlexyBox.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IMediator _mediator;

    public RestaurantService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ResturantDto?> GetRestaurantAsync(int id)
    {
        try
        {
            var query = new GetResturantByIdQuery { Id = id };
            var restaurant = await _mediator.Send(query);
            return restaurant;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Error retrieving restaurant with id {id}", ex);
        }
    }

    public async Task<bool> ToggleFavoriteAsync(int id)
    {
        try
        {
            // For now, just return true - implement actual favorite logic later
            // TODO: Implement ToggleFavoriteCommand when you create the favorites feature
            await Task.Delay(1); // Simulate async operation
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Error toggling favorite for restaurant {id}", ex);
        }
    }

    public async Task<bool> IsFavoriteAsync(int id)
    {
        try
        {
            // For now, return false - implement actual logic later
            // TODO: Implement GetIsFavoriteQuery when you create the favorites feature
            await Task.Delay(1); // Simulate async operation
            return false;
        }
        catch (Exception ex)
        {
            // Log the exception and return false as default
            return false;
        }
    }
}
