using Hotelos.Domain.Hotels.ValueObjects;
using Hotelos.Domain.Rooms.Entities.Floors;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Hotels
{
    public sealed class Hotel : FullAuditedAggregateRoot<int>
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? Description { get; private set; }
        public string? LogoUrl { get; private set; }
        public Guid UserId { get; private set; }
        public List<Floor> Floors { get; private set; }

        private Hotel()
        {
        }

        public static Hotel Create(string name,
            string state,
            string city,
            string street,
            string? phoneNumber,
            string? email,
            string? description,
            Guid userId)
        {
            return new Hotel()
            {
                Name = name,
                Address = new Address(state, city, street),
                PhoneNumber = phoneNumber,
                Email = email,
                Description = description,
                UserId = userId,
                CreationTime = DateTime.Now,
                CreatorId = userId,
                LastModificationTime = DateTime.Now,
                LastModifierId = userId,
            };
        }

        public void Update(string name,
            string state,
            string city,
            string street,
            string phoneNumber,
            string email,
            string? description)
        {

            Name = name;
            Address = new Address(state, city, street);
            PhoneNumber = phoneNumber;
            Email = email;
            Description = description;
            LastModificationTime = DateTime.Now;
        }

        public void SetLogo(string logoUrl)
        {
            LogoUrl = logoUrl;
            LastModificationTime = DateTime.Now;
        }
    }

}
