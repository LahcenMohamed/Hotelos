using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Reservations.ReservationServices
{
    public sealed class ReservationService : FullAuditedEntity<int>
    {
        public int ReservationId { get; private set; }
        public int ServiceId { get; private set; }

        private ReservationService()
        {
        }

        public static ReservationService Create(int reservationId, int serviceId, Guid userId)
        {
            return new ReservationService
            {
                ReservationId = reservationId,
                ServiceId = serviceId,
                CreationTime = DateTime.Now,
                CreatorId = userId
            };
        }
    }
}
