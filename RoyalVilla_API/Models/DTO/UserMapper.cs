using Riok.Mapperly.Abstractions;
using RoyalVilla_API.Models.DTO.VillaDTOs;

namespace RoyalVilla_API.Models.DTO
{
    [Mapper]
    public partial class UserMapper
    {
        //public partial void CopyToUserDTO(User source, UserDTO target);

        public partial UserDTO ToUserDTO(User source);

        public partial void CopyToUser(UserDTO source, User target);
    }
}
