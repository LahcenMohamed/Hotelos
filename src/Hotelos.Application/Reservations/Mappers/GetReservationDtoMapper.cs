using Hotelos.Application.Clients.Mappers;
using Hotelos.Application.Contracts.Reservations.Dtos;
using Hotelos.Domain.Reservations;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Reservations.Mappers
{
    [Mapper]
    public partial class GetReservationDtoMapper
    {
        [UseMapper]
        private readonly GetClientDtoMapper _getClientDtoMapper = new();
        public partial GetReservationDto ToDto(Reservation reservation);
    }

    [Mapper]
    [UseStaticMapper(typeof(GetClientsDtoMapper))]
    public static partial class GetReservationsDtoMapper
    {
        public static partial IQueryable<GetReservationDto> ToDto(this IQueryable<Reservation> reservations);
    }
}
