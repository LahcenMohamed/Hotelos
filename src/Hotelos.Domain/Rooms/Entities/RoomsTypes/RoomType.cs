using Hotelos.Domain.Common.Entity;
using System;

namespace Hotelos.Domain.Rooms.Entities.RoomsTypes
{
    public sealed class RoomType : HotelEntityBase
    {
        public string Name { get; private set; }

        private RoomType()
        {

        }

        public static RoomType Create(string name,
                                   int hotelId,
                                   Guid userId)
        {
            return new RoomType
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
