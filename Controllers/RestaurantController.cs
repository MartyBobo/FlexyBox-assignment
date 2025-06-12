using Application.DTOs;
using FlexyBox.Controllers.Models.Requests;
using FlexyBox.Controllers.Models.Responses;
using FlexyBox.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexyBox.Controllers;

/// <summary>
/// API controller for restaurant-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    /// <summary>
    /// Get all restaurants
    /// </summary>
    /// <returns>List of all restaurants</returns>
    /// <response code="200">Returns the list of restaurants</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ResturantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ResturantDto>>> GetAllRestaurants()
    {
        try
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving restaurants.", ex.Message));
        }
    }

    /// <summary>
    /// Get a specific restaurant by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResturantDto>> GetRestaurant(int id)
    {
        try
        {
            var restaurant = await _restaurantService.GetRestaurantAsync(id);
            
            if (restaurant == null)
            {
                return NotFound(new ErrorResponse($"Restaurant with ID {id} not found."));
            }
            
            return Ok(restaurant);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving the restaurant.", ex.Message));
        }
    }

    /// <summary>
    /// Search restaurants by name or cuisine
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<List<ResturantDto>>> SearchRestaurants([FromQuery] string term)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(new ErrorResponse("Search term is required."));
            }

            var restaurants = await _restaurantService.SearchRestaurantsAsync(term);
            return Ok(restaurants);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while searching restaurants.", ex.Message));
        }
    }

    /// <summary>
    /// Get user's favorite restaurants
    /// </summary>
    [HttpGet("favorites")]
    public async Task<ActionResult<List<ResturantDto>>> GetFavorites()
    {
        try
        {
            var favorites = await _restaurantService.GetFavoritesAsync();
            return Ok(favorites);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving favorites.", ex.Message));
        }
    }

    /// <summary>
    /// Toggle favorite status for a restaurant
    /// </summary>
    [HttpPost("{id}/favorite")]
    public async Task<IActionResult> ToggleFavorite(int id)
    {
        try
        {
            var result = await _restaurantService.ToggleFavoriteAsync(id);
            return Ok(new { isFavorite = result, message = result ? "Added to favorites" : "Removed from favorites" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while toggling favorite.", ex.Message));
        }
    }

    /// <summary>
    /// Check if a restaurant is favorited by the user
    /// </summary>
    [HttpGet("{id}/favorite")]
    public async Task<ActionResult<bool>> IsFavorite(int id)
    {
        try
        {
            var isFavorite = await _restaurantService.IsFavoriteAsync(id);
            return Ok(new { isFavorite });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while checking favorite status.", ex.Message));
        }
    }

    /// <summary>
    /// Get all reviews for a restaurant
    /// </summary>
    [HttpGet("{id}/reviews")]
    public async Task<ActionResult<List<ReviewDto>>> GetRestaurantReviews(int id)
    {
        try
        {
            var reviews = await _restaurantService.GetRestaurantReviewsAsync(id);
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving reviews.", ex.Message));
        }
    }

    /// <summary>
    /// Create a new review for a restaurant
    /// </summary>
    [HttpPost("{id}/reviews")]
    public async Task<ActionResult<ReviewDto>> CreateReview(int id, [FromBody] CreateReviewRequest request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new ErrorResponse("Review data is required."));
            }

            if (request.Rating < 1 || request.Rating > 5)
            {
                return BadRequest(new ErrorResponse("Rating must be between 1 and 5."));
            }

            if (string.IsNullOrWhiteSpace(request.Comment))
            {
                return BadRequest(new ErrorResponse("Comment is required."));
            }

            var review = await _restaurantService.CreateReviewAsync(id, request.Rating, request.Comment);
            return CreatedAtAction(nameof(GetRestaurantReviews), new { id }, review);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while creating the review.", ex.Message));
        }
    }
}
