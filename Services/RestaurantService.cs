using Application.DTOs;
using Application.Resturants.Queries;
using Application.Queries;
using Application.Commands;
using MediatR;

namespace FlexyBox.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUser;

    public RestaurantService(IMediator mediator, ICurrentUserService currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
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
            var command = new ToggleFavoriteCommand { UserId = _currentUser.UserId, RestaurantId = id };
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
            var query = new IsFavoriteQuery { UserId = _currentUser.UserId, RestaurantId = id };
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception)
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

    public async Task<List<ResturantDto>> GetFavoritesAsync()
    {
        try
        {
            var query = new GetFavoritesByUserQuery { UserId = _currentUser.UserId };
            var favorites = await _mediator.Send(query);
            return favorites;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception($"Error retrieving favorites for user {_currentUser.UserId}", ex);
        }
    }

    public async Task<List<ResturantDto>> GetAllRestaurantsAsync()
    {
        try
        {
            var query = new GetAllRestaurantsQuery();
            var restaurants = await _mediator.Send(query);
            return restaurants;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new Exception("Error retrieving all restaurants", ex);
        }
    }
}
