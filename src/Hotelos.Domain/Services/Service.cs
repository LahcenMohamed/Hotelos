using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Services
{
    public sealed class Service : FullAuditedAggregateRoot<int>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }
        public int HotelId { get; private set; }

        private Service()
        {

        }

        public static Service Create(string name,
                                     decimal price,
                                     Guid userId,
                                     int hotelId,
                                     string? descritpion)
        {
            return new Service
            {
                Name = name,
                Price = price,
                HotelId = hotelId,
                CreatorId = userId,
                Description = descritpion,
                CreationTime = DateTime.Now
            };
        }

        public void Update(string name,
                           decimal price,
                           string? descritpion,
                           Guid userId)
        {
            Name = name;
            Price = price;
            LastModifierId = userId;
            Description = descritpion;
            LastModificationTime = DateTime.Now;
        }
    }
}
