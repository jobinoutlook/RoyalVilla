using Riok.Mapperly.Abstractions;

namespace RoyalVilla_API.Models.DTO
{
    [Mapper]
    public partial class VillaMapper
    {


        public partial Villa ToEntity(VillaCreateDTO dto);

        public partial Villa ToEntity(VillaUpdateDTO dto);

        public partial void UpdateVilla(VillaUpdateDTO source, Villa target);
    }
}
