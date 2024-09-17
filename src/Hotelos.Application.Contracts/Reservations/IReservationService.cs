using Hotelos.Application.Contracts.Reservations.Dtos;
using Hotelos.Domain.Shared.Reservations.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Reservations
{
    public interface IReservationService
    {
        Task<GetReservationDto> Create(CreateReservationDto createReservationDto);
        Task PatchType(int id, ReservationType reservationType);
        Task<GetReservationDto> Update(UpdateReservationDto updateReservationDto);
        Task UpdateEntryDate(UpdateEntryDateReservationDto updateEntryDateReservationDto);
        Task UpdateExitDate(UpdateExitDateReservationDto updateExitDateReservationDto);
        Task Delete(int id);
        Task<List<GetReservationDto>> GetAll(GetReservationRequestDto getRoomsRequestDto);
    }
}
