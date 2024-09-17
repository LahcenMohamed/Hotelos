using Hotelos.Domain.Common.Entity;
using Hotelos.Domain.Common.ValueObjects;
using System;

namespace Hotelos.Domain.Clients
{
    public sealed class Client : HotelAggragateRootBase
    {
        public FullName FullName { get; private set; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Description { get; private set; }
        private Client()
        {
        }

        public static Client Create(string firstName,
                                    string? middleName,
                                    string lastName,
                                    int hotelId,
                                    Guid userId,
                                    string? email,
                                    string? phoneNumber,
                                    string? description)
        {
            return new Client
            {
                FullName = new FullName(firstName, middleName, lastName),
                HotelId = hotelId,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = description,
                CreationTime = DateTime.Now,
                CreatorId = userId
            };
        }

        public void Update(string firstName,
                           string? middleName,
                           string lastName,
                           Guid userId,
                           string? email,
                           string? phoneNumber,
                           string? description)
        {
            FullName = new FullName(firstName, middleName, lastName);
            Email = email;
            PhoneNumber = phoneNumber;
            Description = description;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }
    }
}
