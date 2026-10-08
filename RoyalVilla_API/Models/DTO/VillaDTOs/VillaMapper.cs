using Riok.Mapperly.Abstractions;
using RoyalVilla_API.Models.DTO;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.Models.DTO.VillaDTOs
{
    [Mapper]
    public partial class VillaMapper
    {

        [MapperIgnoreTarget(nameof(Villa.Id))]
        [MapperIgnoreTarget(nameof(Villa.CreatedDate))]
        [MapperIgnoreTarget(nameof(Villa.UpdatedDate))]
        [MapperIgnoreTarget(nameof(Villa.Amenities))]
        public partial Villa ToEntity(VillaCreateDTO dto);

        //public partial VillaDTO ToDTO(VillaCreateDTO dto);

        //public partial Villa ToEntity(VillaUpdateDTO dto);

        public partial VillaDTO ToDTO(VillaUpdateDTO dto);

        [MapperIgnoreTarget(nameof(Villa.CreatedDate))]
        [MapperIgnoreTarget(nameof(Villa.UpdatedDate))]
        [MapperIgnoreTarget(nameof(Villa.Amenities))]
        public partial Villa UpdateVilla(VillaUpdateDTO source);

        [MapperIgnoreSource(nameof(Villa.CreatedDate))]
        [MapperIgnoreSource(nameof(Villa.UpdatedDate))]
        [MapperIgnoreSource(nameof(Villa.Amenities))]
        public partial VillaDTO ToDTO(Villa entity);

        //public partial Villa ToEntity(VillaDTO dto);

        public partial List<VillaDTO> ToDTOList(List<Villa> entities);

    }
}
