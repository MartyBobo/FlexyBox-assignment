using Application.DTOs;

namespace FlexyBox.Services;

public interface IRestaurantService
{
    Task<List<ResturantDto>> GetAllRestaurantsAsync();
    Task<ResturantDto?> GetRestaurantAsync(int id);
    Task<bool> ToggleFavoriteAsync(int id);
    Task<bool> IsFavoriteAsync(int id);
    Task<List<ResturantDto>> SearchRestaurantsAsync(string searchTerm);
    Task<List<ResturantDto>> GetFavoritesAsync();
    Task<List<ReviewDto>> GetRestaurantReviewsAsync(int restaurantId);
    Task<ReviewDto> CreateReviewAsync(int restaurantId, int rating, string comment);
}
