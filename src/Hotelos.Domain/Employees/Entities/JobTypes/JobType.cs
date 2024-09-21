using Hotelos.Domain.Common.Entity;
using System;

namespace Hotelos.Domain.Employees.Entities.JobTypes
{
    public sealed class JobType : HotelEntityBase
    {
        public string Title { get; private set; }

        private JobType()
        {
        }

        public static JobType Create(string titel,
                                     int hotelId,
                                     Guid userId)
        {
            return new JobType
            {
                Title = titel,
                HotelId = hotelId,
                CreatorId = userId,
                CreationTime = DateTime.Now
            };
        }

        public void Update(string titel,
                           Guid userId)
        {
            Title = titel;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }
    }
}
