using Application.DTOs;

namespace FlexyBox.Services;

public interface IRestaurantService
{
    Task<ResturantDto?> GetRestaurantAsync(int id);
    Task<bool> ToggleFavoriteAsync(int id);
    Task<bool> IsFavoriteAsync(int id);
    Task<List<ResturantDto>> SearchRestaurantsAsync(string searchTerm);
    Task<List<ResturantDto>> GetFavoritesAsync();
}
