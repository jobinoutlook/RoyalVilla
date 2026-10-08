using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models.DTO;
using RoyalVilla_API.Models.DTO.VillaAmenitiesDTOs;
using RoyalVilla_API.Models.DTO.VillaDTOs;

namespace RoyalVilla_API.Controllers
{
    [Route("api/villa-amenities")]
    [ApiController]
    public class VillaAmenitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly VillaAmenitiesMapper _mapper;

        public VillaAmenitiesController(ApplicationDbContext db,VillaAmenitiesMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<VillaAmenitiesDTO>>), StatusCodes.Status200OK)]   //for documentation
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<VillaAmenitiesDTO>>>> GetVillaAmenities()
        {
            var villaAmenities = await _db.VillaAmenities.ToListAsync();

            var response = ApiResponse<IEnumerable<VillaAmenitiesDTO>>.Ok(_mapper.ToDTOList(villaAmenities), "Villa amenities retrieved successfully.");

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        //[AllowAnonymous]
        public async Task<ActionResult<ApiResponse<VillaAmenitiesDTO>>> GetVillaAmenityById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponse<object?>.BadRequest("Invalid villa ID. ID must be greater than zero."));
                }

                var villaAmenity = await _db.VillaAmenities.FindAsync(id);
                if (villaAmenity == null)
                {
                    return NotFound(ApiResponse<object?>.NotFound($"Villa with ID {id} not found."));
                }
                return Ok(ApiResponse<VillaAmenitiesDTO>.Ok(_mapper.ToDTO(villaAmenity), "Villa retrieved successfully."));
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occurred while retrieving the villa", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<VillaAmenitiesDTO>>> CreateVillaAmenity(VillaAmenitiesCreateDTO villaAmenitiesDTO)
        {
            try
            {
                if (villaAmenitiesDTO == null)
                {
                    return BadRequest(ApiResponse<object?>.BadRequest("Villa amenity data is required."));
                }

                var villaExist = await _db.Villa.AnyAsync(v => v.Id == villaAmenitiesDTO.VillaId);
                if(!villaExist)
                {
                    return Conflict(ApiResponse<object>.Conflict($"Villa with ID {villaAmenitiesDTO.VillaId} not found."));
                }

                var villaAmenity = _mapper.ToEntity(villaAmenitiesDTO);
                villaAmenity.CreatedDate = DateTime.Now;

                await _db.VillaAmenities.AddAsync(villaAmenity);
                await _db.SaveChangesAsync();

                var response = ApiResponse<VillaAmenitiesDTO>.CreatedAt(_mapper.ToDTO(villaAmenity), "Villa amenity created successfully.");
                return CreatedAtAction(nameof(CreateVillaAmenity), new { id = villaAmenity.Id }, response);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occurred while retrieving the villa", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<VillaAmenitiesDTO>>> UpdateVillaAmenity(int id, VillaAmenitiesUpdateDTO villaAmenityDTO)
        {
            try
            {
                if (villaAmenityDTO == null)
                {
                    return BadRequest(ApiResponse<object?>.BadRequest("Villa amenity data is required."));

                }

                if (id != villaAmenityDTO.Id)
                {
                    return BadRequest(ApiResponse<object?>.BadRequest("Villa amenity ID in the URL does not match the ID in the request body."));
                }

                var existingVillaAmenity = await _db.VillaAmenities.FindAsync(id);

                if (existingVillaAmenity == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Villa amenity with ID {id} not found."));
                }

                

                _mapper.UpdateVillaAmenitiesDTO(villaAmenityDTO, existingVillaAmenity);
                existingVillaAmenity.UpdatedDate = DateTime.Now;
                 
                await _db.SaveChangesAsync();
                return Ok(ApiResponse<VillaAmenitiesDTO>.Ok(_mapper.ToDTO(existingVillaAmenity), "Villa amenity updated successfully."));

            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occurred while retrieving the villa", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteVillaAmenity(int id)
        {
            try
            {


                if (id <= 0)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Invalid Villa amenity ID"));
                }

                var existingVilla = await _db.VillaAmenities.FindAsync(id);

                if (existingVilla == null)
                {
                    return NotFound(ApiResponse<object>.NotFound($"Villa with ID {id} not found."));
                }

                _db.VillaAmenities.Remove(existingVilla);

                await _db.SaveChangesAsync();

                var response = ApiResponse<object>.NoContent("Villa amenity deleted successfully.");
                return Ok(response);

            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<object>.Error(500, $"An error occurred while retrieving the villa amenity", ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

    }
}
