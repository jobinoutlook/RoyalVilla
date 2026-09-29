using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models;
using System.Collections;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/villa")]
    public class VillaController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public VillaController(ApplicationDbContext db)
        {
            this.db = db;
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


    }
}
