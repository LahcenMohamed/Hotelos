using Hotelos.Application.Contracts.RoomTypes.Dtos;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.RoomTypes.Mappers
{
    [Mapper]
    public static partial class GetRoomTypesMapper
    {
        public static partial IQueryable<GetRoomTypeDto> ToDto(this IQueryable<RoomType> roomTypes);
    }
}
