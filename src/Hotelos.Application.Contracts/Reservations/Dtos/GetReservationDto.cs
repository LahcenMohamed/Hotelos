using Hotelos.Application.Contracts.Clients.Dtos;
using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Domain.Shared.Reservations.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Reservations.Dtos
{
    public sealed class GetReservationDto : EntityDto<int>
    {
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal RestPrice { get; set; }
        public int CountOfPeople { get; set; }
        public int ClientId { get; set; }
        public ReservationType Type { get; set; }
        public int RoomId { get; set; }
        public GetClientDto Client { get; set; }
        public GetRoomDto Room { get; set; }
    }
}
