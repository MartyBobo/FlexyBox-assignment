using Application.DTOs;
using FlexyBox.Controllers.Models.Requests;
using FlexyBox.Controllers.Models.Responses;
using FlexyBox.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexyBox.Controllers;

/// <summary>
/// API controller for user-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get the current user's profile
    /// </summary>
    [HttpGet("profile")]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        try
        {
            var profile = await _userService.GetProfileAsync();
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving the profile.", ex.Message));
        }
    }

    /// <summary>
    /// Update the current user's profile
    /// </summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest(new ErrorResponse("Profile data is required."));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new ErrorResponse("Name is required."));
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new ErrorResponse("Email is required."));
            }

            var userDto = new UserDto
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };

            await _userService.UpdateProfileAsync(userDto);
            return Ok(new { message = "Profile updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while updating the profile.", ex.Message));
        }
    }

    /// <summary>
    /// Get all reviews by the current user
    /// </summary>
    [HttpGet("reviews")]
    public async Task<ActionResult<List<UserReviewDto>>> GetUserReviews()
    {
        try
        {
            var reviews = await _userService.GetUserReviewsAsync();
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while retrieving reviews.", ex.Message));
        }
    }

    /// <summary>
    /// Update a review by the current user
    /// </summary>
    [HttpPut("reviews/{reviewId}")]
    public async Task<ActionResult<ReviewDto>> UpdateReview(int reviewId, [FromBody] UpdateReviewRequest request)
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

            var updatedReview = await _userService.UpdateReviewAsync(reviewId, request.Rating, request.Comment);
            return Ok(updatedReview);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while updating the review.", ex.Message));
        }
    }

    /// <summary>
    /// Delete a review by the current user
    /// </summary>
    [HttpDelete("reviews/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        try
        {
            var result = await _userService.DeleteReviewAsync(reviewId);
            
            if (result)
            {
                return Ok(new { message = "Review deleted successfully." });
            }
            
            return NotFound(new ErrorResponse("Review not found."));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse("An error occurred while deleting the review.", ex.Message));
        }
    }
}