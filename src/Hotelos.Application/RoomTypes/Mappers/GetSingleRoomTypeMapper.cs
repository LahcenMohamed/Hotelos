using Hotelos.Application.Contracts.RoomTypes.Dtos;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Riok.Mapperly.Abstractions;

namespace Hotelos.Application.RoomTypes.Mappers
{
    [Mapper]
    public partial class GetSingleRoomTypeMapper
    {
        public partial GetRoomTypeDto ToDto(RoomType roomType);
    }
}
