using Application.DTOs;
using Application.Resturants.Queries;
using Application.Queries;
using Application.Commands;
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
    }    public async Task<bool> ToggleFavoriteAsync(int id)
    {
        try
        {
            var command = new ToggleFavoriteCommand { RestaurantId = id };
            var result = await _mediator.Send(command);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Error toggling favorite for restaurant {id}", ex);
        }
    }    public async Task<bool> IsFavoriteAsync(int id)
    {
        try
        {
            var query = new IsFavoriteQuery { RestaurantId = id };
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception and return false as default
            return false;
        }
    }

    public async Task<List<ResturantDto>> SearchRestaurantsAsync(string searchTerm)
    {
        try
        {
            var query = new SearchRestaurantsQuery { SearchTerm = searchTerm };
            var restaurants = await _mediator.Send(query);
            return restaurants;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Error searching restaurants with term '{searchTerm}'", ex);
        }
    }
}
