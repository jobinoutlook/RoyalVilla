using Riok.Mapperly.Abstractions;
using RoyalVilla_API.Models.DTO.VillaDTOs;

namespace RoyalVilla_API.Models.DTO.VillaAmenitiesDTOs
{
    [Mapper]
    public partial class VillaAmenitiesMapper
    {

        [MapperIgnoreTarget(nameof(VillaAmenities.Id))]
        [MapperIgnoreTarget(nameof(VillaAmenities.CreatedDate))]
        [MapperIgnoreTarget(nameof(VillaAmenities.UpdatedDate))]
        [MapperIgnoreTarget(nameof(VillaAmenities.Villa))]
        public partial VillaAmenities ToEntity(VillaAmenitiesCreateDTO dto);

        [MapperIgnoreTarget(nameof(VillaAmenitiesDTO.VillaName))]
        public partial VillaAmenitiesDTO ToDTO(VillaAmenitiesUpdateDTO dto);

        [MapperIgnoreSource(nameof(VillaAmenities.CreatedDate))]
        [MapperIgnoreSource(nameof(VillaAmenities.UpdatedDate))]
        //[MapperIgnoreSource(nameof(VillaAmenities.Villa))]
        //[MapperIgnoreTarget(nameof(VillaAmenitiesDTO.VillaName))]
        [MapProperty(nameof(VillaAmenities.Villa.Name), nameof(VillaAmenitiesDTO.VillaName))]
        public partial VillaAmenitiesDTO ToDTO(VillaAmenities entity);

        public partial List<VillaAmenitiesDTO> ToDTOList(List<VillaAmenities> entities);
    }
}
