using Hotelos.Domain.Common.Entity;
using System;

namespace Hotelos.Domain.Rooms.Entities.Floors
{
    public sealed class Floor : HotelEntityBase
    {
        public string Name { get; private set; }

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
