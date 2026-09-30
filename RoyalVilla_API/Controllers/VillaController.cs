
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models;
using RoyalVilla_API.Models.DTO;
using System.Collections;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/villa")]
    public class VillaController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly VillaMapper mapper;

        //private readonly IMapper mapper;

        public VillaController(ApplicationDbContext db,VillaMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
            //this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
        {
            return Ok(await db.Villa.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Villa>> GetVillaById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Invalid villa ID. ID must be greater than zero.");
                }   

                var villa = await db.Villa.FindAsync(id);
                if (villa == null)
                {
                    return NotFound($"Villa with ID {id} not found.");
                }
                return Ok(villa);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while retrieving the villa with Id {id}: {ex.Message}");
            }
        }

        //[HttpGet("{id:int}/{name}")]
        //public string GetVillaByIdAndName([FromRoute] int id,[FromRoute] string name)
        //{
        //    return $"Get Villa with ID: {id} and Name: {name}";
        //}

        [HttpPost]
        public async Task<ActionResult<Villa>> CreateVilla(VillaCreateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Villa data is required.");
                }

                var villa = mapper.ToEntity(villaDTO);

                await db.Villa.AddAsync(villa);
                await db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetVillaById), new { id = villa.Id }, villa);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while creating the villa: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Villa>> UpdateVilla(int id, VillaUpdateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Villa data is required.");
                }

                if (id != villaDTO.Id)
                {
                    return BadRequest("Villa ID in the URL does not match the ID in the request body.");
                }

                var existingVilla = await db.Villa.FindAsync(id);

                if (existingVilla == null)
                {
                    return NotFound($"Villa with ID {id} not found.");
                }

                mapper.UpdateVilla(villaDTO, existingVilla);
                existingVilla.UpdatedDate = DateTime.Now;

                await db.SaveChangesAsync();
                return Ok(villaDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occurred while updating the villa: {ex.Message}");
            }
        }

    }
}
