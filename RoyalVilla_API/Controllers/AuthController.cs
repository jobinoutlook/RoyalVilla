using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalVilla_API.Models.DTO;
using RoyalVilla_API.Models.DTO.VillaDTOs;

namespace RoyalVilla_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
                
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<VillaDTO>>), StatusCodes.Status200OK)]   //for documentation
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Register()
        {
            //Auth Service

            var response = ApiResponse<UserDTO>.Ok(null, "User created successfully");

            return Ok(response);
        }
    }
}
