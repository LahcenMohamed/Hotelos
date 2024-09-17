using Hotelos.Domain.Shared.Reservations.Enums;
using System;
using System.Collections.Generic;

namespace Hotelos.Application.Contracts.Reservations.Dtos
{
    public sealed record CreateReservationDto(DateTime EntryDate,
                                              DateTime ExitDate,
                                              decimal TotalPrice,
                                              decimal RestPrice,
                                              int CountOfPeople,
                                              ReservationType reservationType,
                                              int ClientId,
                                              int RoomId,
                                              List<int> ServicesIds);
}
