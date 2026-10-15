using Riok.Mapperly.Abstractions;
using RoyalVilla.DTO.VillaDTOs;

namespace RoyalVillaWeb.Models
{
    [Mapper]
    public partial class VillaMapper
    {

        [MapperIgnoreTarget(nameof(VillaDTO.Id))]
        //[MapperIgnoreTarget(nameof(Villa.Amenities))]
        public partial VillaDTO ToVillaDTO(VillaCreateDTO dto);

        [MapperIgnoreSource(nameof(VillaDTO.Id))]
        public partial VillaCreateDTO ToCreateDTO(VillaDTO dto);

        public partial VillaDTO ToVillaDTO(VillaUpdateDTO dto);

        public partial VillaUpdateDTO ToUpdateDTO(VillaDTO dto);

        //public partial Villa ToEntity(VillaUpdateDTO dto);

        //public partial VillaDTO ToDTO(VillaUpdateDTO dto);

        //[MapperIgnoreTarget(nameof(Villa.CreatedDate))]
        //[MapperIgnoreTarget(nameof(Villa.UpdatedDate))]
        //[MapperIgnoreTarget(nameof(Villa.Amenities))]
        //public partial void UpdateVilla(VillaUpdateDTO source,Villa target);

        //[MapperIgnoreSource(nameof(Villa.CreatedDate))]
        //[MapperIgnoreSource(nameof(Villa.UpdatedDate))]
        //[MapperIgnoreSource(nameof(Villa.Amenities))]
        //public partial VillaDTO ToDTO(Villa entity);

        ////public partial Villa ToEntity(VillaDTO dto);

        //public partial List<VillaDTO> ToDTOList(List<Villa> entities);

    }
}
