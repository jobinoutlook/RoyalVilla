using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalVilla_API.Models.DTO;
using RoyalVilla_API.Models.DTO.VillaDTOs;
using RoyalVilla_API.Services;

namespace RoyalVilla_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<VillaDTO>>), StatusCodes.Status200OK)]   //for documentation
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            try
            {
                if (registerationRequestDTO == null)
                {
                    var errorResponse = ApiResponse<object>.BadRequest("Registration data is required");
                    return BadRequest(errorResponse);
                }

                if (await _authService.IsEmailExistsAsync(registerationRequestDTO.Email))
                {
                    var errorResponse = ApiResponse<object>.Conflict("User with email already exists");
                    return Conflict(errorResponse);
                }

                var user = await _authService.RegisterAsync(registerationRequestDTO);

                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Registration failed"));
                }

                //Auth Service

                var response = ApiResponse<UserDTO>.CreatedAt(user, "User created successfully");

                return CreatedAtAction(nameof(Register), response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error(StatusCodes.Status500InternalServerError, "An error occurred during registration", new { ExceptionMessage = ex.Message });
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
