using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models.DTO.VillaAmenitiesDTOs
{
    public class VillaAmenitiesUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int VillaId { get; set; }
    }
}
