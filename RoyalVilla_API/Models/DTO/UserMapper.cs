using Riok.Mapperly.Abstractions;
using RoyalVilla_API.Models.DTO.VillaDTOs;

namespace RoyalVilla_API.Models.DTO
{
    [Mapper]
    public partial class UserMapper
    {
        //public partial void CopyToUserDTO(User source, UserDTO target);
        [MapperIgnoreSource(nameof(User.Password))]
        [MapperIgnoreSource(nameof(User.CreatedDate))]
        [MapperIgnoreSource(nameof(User.UpdatedDate))]
        public partial UserDTO ToUserDTO(User source);

        [MapperIgnoreTarget(nameof(User.Password))]
        [MapperIgnoreTarget(nameof(User.CreatedDate))]
        [MapperIgnoreTarget(nameof(User.UpdatedDate))]
        public partial void CopyToUser(UserDTO source, User target);
    }
}
