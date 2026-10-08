using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models.DTO.VillaAmenitiesDTOs
{
    public class VillaAmenitiesCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int VillaId { get; set; }
    }
}
