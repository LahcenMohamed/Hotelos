using System;
using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Reservations.Dtos
{
    public sealed class UpdateEntryDateReservationDto : EntityDto<int>
    {
        public DateTime EntryDate { get; set; }
    }

    public sealed class UpdateExitDateReservationDto : EntityDto<int>
    {
        public DateTime ExitDate { get; set; }
    }
}
