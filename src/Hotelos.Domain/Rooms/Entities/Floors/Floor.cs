using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Rooms.Entities.Floors
{
    public sealed class Floor : FullAuditedEntity<int>
    {
        public string Name { get; private set; }
        public int HotelId { get; private set; }

        private Floor()
        {

        }

        public static Floor Create(string name,
                                   int hotelId,
                                   Guid userId)
        {
            return new Floor
            {
                Name = name,
                LastModificationTime = DateTime.Now,
                CreationTime = DateTime.Now,
                CreatorId = userId,
                LastModifierId = userId,
                HotelId = hotelId,
            };
        }

        public void Update(string name, Guid userId)
        {
            Name = name;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }
    }
}
