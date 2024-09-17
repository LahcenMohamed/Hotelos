using Hotelos.Domain.Clients;
using Hotelos.Domain.Common.Entity;
using Hotelos.Domain.Rooms;
using Hotelos.Domain.Shared.Reservations.Enums;
using System;

namespace Hotelos.Domain.Reservations
{
    public sealed class Reservation : HotelAggragateRootBase
    {
        public DateTime EntryDate { get; private set; }
        public DateTime ExitDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal RestPrice { get; private set; }
        public int CountOfPeople { get; private set; }
        public ReservationType Type { get; private set; }
        public int ClientId { get; private set; }
        public int RoomId { get; private set; }
        public Client Client { get; private set; }
        public Room Room { get; private set; }

        private Reservation()
        {
        }

        public static Reservation Create(DateTime entryDate,
                                         DateTime exitDate,
                                         decimal totalPrice,
                                         decimal restPrice,
                                         int countOfPeople,
                                         ReservationType type,
                                         int clientId,
                                         int roomId,
                                         int hotelId,
                                         Guid userId)
        {
            return new Reservation
            {
                EntryDate = entryDate,
                ExitDate = exitDate,
                TotalPrice = totalPrice,
                RestPrice = restPrice,
                CountOfPeople = countOfPeople,
                Type = type,
                ClientId = clientId,
                RoomId = roomId,
                HotelId = hotelId,
                CreatorId = userId,
                CreationTime = DateTime.Now
            };
        }

        public void Update(decimal totalPrice,
                           decimal restPrice,
                           int countOfPeople,
                           int clientId,
                           int roomId,
                           Guid userId)
        {
            TotalPrice = totalPrice;
            RestPrice = restPrice;
            CountOfPeople = countOfPeople;
            ClientId = clientId;
            RoomId = roomId;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void EditRestPrice(decimal restPrice, Guid userId)
        {
            RestPrice = restPrice;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void EditEntryDate(DateTime entryDate, Guid userId)
        {
            EntryDate = entryDate;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void EditExitDate(DateTime exitDate, Guid userId)
        {
            ExitDate = exitDate;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void EditReservationType(ReservationType type, Guid userId)
        {
            Type = type;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void EditRoomAndClient(Room room, Client client)
        {
            Client = client;
            Room = room;
        }
    }
}
