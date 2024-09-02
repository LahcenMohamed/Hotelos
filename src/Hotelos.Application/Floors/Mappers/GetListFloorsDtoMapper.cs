using Hotelos.Application.Contracts.Floors.Dtos;
using Hotelos.Domain.Rooms.Entities.Floors;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Floors.Mappers
{
    [Mapper]
    public static partial class GetListFloorsDtoMapper
    {
        public static partial IQueryable<GetFloorDto> ToResult(this IQueryable<Floor> floors);
    }
}
