using Hotelos.Domain.Common.Entity;
using System;

namespace Hotelos.Domain.Rooms
{
    public sealed class Room : HotelAggragateRootBase
    {
        public int Number { get; private set; }
        public string? Name { get; private set; }
        public int CountOfBeds { get; private set; }
        public decimal PriceOfOneNight { get; private set; }
        public bool IsVacant { get; private set; }
        public string? Description { get; private set; }
        public int FloorId { get; private set; }
        public int RoomTypeId { get; private set; }

        private Room()
        {

        }

        public static Room Create(int number,
                                  int countOfBeds,
                                  decimal priceOfOneNight,
                                  int floorId,
                                  int roomTypeId,
                                  int hotelId,
                                  Guid userId,
                                  string? name = null,
                                  string? description = null)
        {
            return new Room
            {
                Number = number,
                Name = name,
                CountOfBeds = countOfBeds,
                PriceOfOneNight = priceOfOneNight,
                Description = description,
                FloorId = floorId,
                RoomTypeId = roomTypeId,
                HotelId = hotelId,
                IsVacant = true,
                CreationTime = DateTime.Now,
                CreatorId = userId
            };
        }

        public void Update(int number,
                           int countOfBeds,
                           decimal priceOfOneNight,
                           int floorId,
                           int roomTypeId,
                           bool isVacant,
                           Guid userId,
                           string? name = null,
                           string? description = null)
        {
            Number = number;
            Name = name;
            CountOfBeds = countOfBeds;
            PriceOfOneNight = priceOfOneNight;
            Description = description;
            FloorId = floorId;
            IsVacant = isVacant;
            RoomTypeId = roomTypeId;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }

        public void UpdateVacant(bool isVacant)
        {
            IsVacant = isVacant;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateVacantWithUserId(bool isVacant, Guid userId)
        {
            IsVacant = isVacant;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }
    }
}
