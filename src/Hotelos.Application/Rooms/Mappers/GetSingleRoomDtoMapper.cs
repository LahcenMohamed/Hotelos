using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Domain.Rooms;
using Riok.Mapperly.Abstractions;

namespace Hotelos.Application.Rooms.Mappers
{
    [Mapper]
    public partial class GetSingleRoomDtoMapper
    {
        public partial GetRoomDto ToDto(Room room);
    }
}
