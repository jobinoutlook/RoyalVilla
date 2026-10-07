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
        public partial Villa ToEntity(VillaCreateDTO dto);

        //public partial VillaDTO ToDTO(VillaCreateDTO dto);

        //public partial Villa ToEntity(VillaUpdateDTO dto);

        public partial VillaDTO ToDTO(VillaUpdateDTO dto);

        [MapperIgnoreTarget(nameof(Villa.CreatedDate))]
        [MapperIgnoreTarget(nameof(Villa.UpdatedDate))]
        public partial void UpdateVilla(VillaUpdateDTO source, Villa target);

        [MapperIgnoreSource(nameof(Villa.CreatedDate))]
        [MapperIgnoreSource(nameof(Villa.UpdatedDate))]
        public partial VillaDTO ToDTO(Villa entity);

        //public partial Villa ToEntity(VillaDTO dto);

        public partial List<VillaDTO> ToDTOList(List<Villa> entities);

    }
}
