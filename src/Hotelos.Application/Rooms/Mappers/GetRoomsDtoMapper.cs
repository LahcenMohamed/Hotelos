using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Domain.Rooms;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Rooms.Mappers
{
    [Mapper]
    public static partial class GetRoomsDtoMapper
    {
        public static partial IQueryable<GetRoomDto> ToDto(this IQueryable<Room> rooms);
    }
}
