using Microsoft.AspNetCore.Mvc;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/villa")]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public string GetVilla()
        {
            return "Get all Villas";
        }

        [HttpGet("{id:int}")]
        public string GetVillaById(int id)
        {
            return $"Get Villa with ID: {id}";
        }

        [HttpGet("{id:int}/{name}")]
        public string GetVillaByIdAndName([FromRoute] int id,[FromRoute] string name)
        {
            return $"Get Villa with ID: {id} and Name: {name}";
        }


    }
}
