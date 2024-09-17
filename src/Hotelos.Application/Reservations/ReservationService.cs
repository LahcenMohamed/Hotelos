using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Reservations;
using Hotelos.Application.Contracts.Reservations.Dtos;
using Hotelos.Application.Exceptions;
using Hotelos.Application.Reservations.BackgroundJobs.DischargeRooms;
using Hotelos.Application.Reservations.BackgroundJobs.FillRooms;
using Hotelos.Application.Reservations.Mappers;
using Hotelos.Application.Reservations.Validators;
using Hotelos.Domain.Clients;
using Hotelos.Domain.Reservations;
using Hotelos.Domain.Rooms;
using Hotelos.Domain.Shared.Reservations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Reservations
{
    public class ReservationService(IRepository<Reservation> reservationRepository,
                                    IRepository<Room> roomRepository,
                                    IRepository<Client> clientRepository,
                                    IRepository<Domain.Reservations.ReservationServices.ReservationService> reservationServiceRepository,
                                    IDistributedCache<List<GetReservationDto>> reservationsDistributedCache,
                                    IBackgroundJobManager backgroundJobManager) : BaseServices, IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository = reservationRepository;
        private readonly IRepository<Room> _roomRepository = roomRepository;
        private readonly IRepository<Client> _clientRepository = clientRepository;
        private readonly IRepository<Domain.Reservations.ReservationServices.ReservationService> _reservationServiceRepository = reservationServiceRepository;
        private readonly IDistributedCache<List<GetReservationDto>> _reservationsDistributedCache = reservationsDistributedCache;
        private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

        public async Task<GetReservationDto> Create(CreateReservationDto createReservationDto)
        {
            await ValidationErorrResult(new CreateReservationDtoValidator(), createReservationDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var room = await FindAggragateRootAsync(_roomRepository, createReservationDto.RoomId, hotelId, "Room");
            var client = await FindAggragateRootAsync(_clientRepository, createReservationDto.ClientId, hotelId, "Client");

            var lastReservation = await _reservationRepository.FirstOrDefaultAsync(x => x.RoomId == createReservationDto.RoomId &&
                                                                                        ((x.EntryDate <= createReservationDto.EntryDate &&
                                                                                         x.ExitDate >= createReservationDto.EntryDate)
                                                                                         ||
                                                                                         (x.EntryDate <= createReservationDto.ExitDate &&
                                                                                          x.ExitDate >= createReservationDto.ExitDate)));

            if (lastReservation is not null)
            {
                throw new UnprocessableEntityException("this room elready reseraved in this date");
            }

            var reservation = Reservation.Create(createReservationDto.EntryDate,
                                                 createReservationDto.ExitDate,
                                                 createReservationDto.TotalPrice,
                                                 createReservationDto.RestPrice,
                                                 createReservationDto.CountOfPeople,
                                                 createReservationDto.reservationType,
                                                 createReservationDto.ClientId,
                                                 createReservationDto.RoomId,
                                                 hotelId,
                                                 userId);

            await _reservationRepository.InsertAsync(reservation, true);

            var reservationServices = new List<Domain.Reservations.ReservationServices.ReservationService>();
            foreach (var serviceId in createReservationDto.ServicesIds)
            {
                var reservationService = Domain.Reservations.ReservationServices.ReservationService.Create(reservation.Id, serviceId, userId);
                reservationServices.Add(reservationService);
            }
            await _reservationServiceRepository.InsertManyAsync(reservationServices, true);

            if (reservation.Type == ReservationType.Confirmed)
            {
                await UpdateRoomVacentBackground(room, reservation, userId);
            }
            reservation.EditRoomAndClient(room, client);
            var mapper = new GetReservationDtoMapper();
            var reservationDto = mapper.ToDto(reservation);

            await RefreshCache(reservation, (list, dto) => { list.Add(dto); return list; });

            return reservationDto;
        }

        public async Task Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservation = await FindAggragateRootAsync(_reservationRepository, id, hotelId, "Reservation");
            await _reservationRepository.DeleteAsync(reservation, true);
            //await RefreshCache(reservation, (list, dto) =>
            //{
            //    list.RemoveAll(r => r.Id == dto.Id);
            //    return list;
            //});
            //problem with mapperly
        }

        public async Task<List<GetReservationDto>> GetAll(GetReservationRequestDto getRoomsRequestDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservations = await _reservationsDistributedCache.GetOrAddAsync($"GetReservationOfHotel-{hotelId}-AndCLient-{getRoomsRequestDto.ClientId}-AndRoom-{getRoomsRequestDto.RoomId}",
                async () => await GetReservationsFromDb(getRoomsRequestDto.ClientId, getRoomsRequestDto.RoomId, hotelId));
            return reservations;
        }

        public async Task PatchType(int id, ReservationType reservationType)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservation = await _reservationRepository.FirstOrDefaultAsync(x => x.Id == id && x.HotelId == hotelId);

            if (reservation is null)
            {
                throw new EntityNotFoundException();
            }

            reservation.EditReservationType(reservationType, userId);
            await _reservationRepository.UpdateAsync(reservation, true);
            var room = await _roomRepository.FirstOrDefaultAsync(x => x.Id == reservation.RoomId);
            if (reservation.Type == ReservationType.Confirmed)
            {
                await UpdateRoomVacentBackground(room, reservation, userId);
            }
        }

        public async Task<GetReservationDto> Update(UpdateReservationDto updateReservationDto)
        {
            await ValidationErorrResult(new UpdateReservationValidator(), updateReservationDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservation = await FindAggragateRootAsync(_reservationRepository, updateReservationDto.Id, hotelId, "Reservation");
            var room = await FindAggragateRootAsync(_roomRepository, updateReservationDto.RoomId, hotelId, "Room");
            var client = await FindAggragateRootAsync(_clientRepository, updateReservationDto.ClientId, hotelId, "Client");
            reservation.Update(updateReservationDto.TotalPrice,
                               updateReservationDto.RestPrice,
                               updateReservationDto.CountOfPeople,
                               updateReservationDto.ClientId,
                               updateReservationDto.RoomId,
                               userId);
            reservation.EditRoomAndClient(room, client);
            await _reservationRepository.UpdateAsync(reservation, true);
            await RefreshCache(reservation, (list, dto) =>
            {
                var index = list.FindIndex(r => r.Id == dto.Id);
                if (index != -1)
                    list[index] = dto;
                return list;
            });
            var mapper = new GetReservationDtoMapper();
            return mapper.ToDto(reservation);
        }

        public async Task UpdateEntryDate(UpdateEntryDateReservationDto updateEntryDateReservationDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservation = await FindAggragateRootAsync(_reservationRepository, updateEntryDateReservationDto.Id, hotelId, "Reservation");
            var room = await FindAggragateRootAsync(_roomRepository, reservation.RoomId, hotelId, "Room");
            var client = await FindAggragateRootAsync(_clientRepository, reservation.ClientId, hotelId, "Client");
            reservation.EditRoomAndClient(room, client);
            reservation.EditEntryDate(updateEntryDateReservationDto.EntryDate, userId);

            await _reservationRepository.UpdateAsync(reservation, true);
            await RefreshCache(reservation, (list, dto) =>
            {
                var index = list.FindIndex(r => r.Id == dto.Id);
                if (index != -1)
                    list[index].EntryDate = dto.EntryDate;
                return list;
            });

            if (reservation.Type == ReservationType.Confirmed && reservation.EntryDate > DateTime.Now)
            {
                TimeSpan timeUntilEntry = reservation.EntryDate - DateTime.Now;
                await _backgroundJobManager.EnqueueAsync(new FillRoomArgs { Id = reservation.RoomId, ReservationId = reservation.Id }, delay: timeUntilEntry);
            }
        }

        public async Task UpdateExitDate(UpdateExitDateReservationDto updateExitDateReservationDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var reservation = await FindAggragateRootAsync(_reservationRepository, updateExitDateReservationDto.Id, hotelId, "Reservation");

            reservation.EditExitDate(updateExitDateReservationDto.ExitDate, userId);
            var room = await FindAggragateRootAsync(_roomRepository, reservation.RoomId, hotelId, "Room");
            var client = await FindAggragateRootAsync(_clientRepository, reservation.ClientId, hotelId, "Client");
            reservation.EditRoomAndClient(room, client);
            await _reservationRepository.UpdateAsync(reservation, true);
            await RefreshCache(reservation, (list, dto) =>
            {
                var index = list.FindIndex(r => r.Id == dto.Id);
                if (index != -1)
                    list[index].ExitDate = dto.ExitDate;
                return list;
            });

            if (reservation.Type == ReservationType.Confirmed && reservation.ExitDate > DateTime.Now)
            {
                TimeSpan timeUntilExit = reservation.ExitDate - DateTime.Now;
                await _backgroundJobManager.EnqueueAsync(new DischargeRoomArgs { Id = reservation.RoomId, ReservationId = reservation.Id }, delay: timeUntilExit);
            }
        }


        private async Task<List<GetReservationDto>> GetReservationsFromDb(int clientId, int roomId, int hotelId)
        {
            var reservations = await _reservationRepository.WithDetailsAsync(x => x.Room, x => x.Client);
            return reservations.Where(x => x.HotelId == hotelId &&
                                           (x.ClientId == clientId || clientId == 0) &&
                                           (x.RoomId == roomId || roomId == 0))
                               .ToDto()
                               .ToList();
        }
        private async Task UpdateRoomVacentBackground(Room room, Reservation reservation, Guid userId)
        {
            var checkDate = reservation.EntryDate < DateTime.Now && reservation.ExitDate > DateTime.Now;

            if (checkDate)
            {
                room.UpdateVacantWithUserId(true, userId);
                await _roomRepository.UpdateAsync(room, true);
                TimeSpan timeUntilExit = reservation.ExitDate - DateTime.Now;
                await _backgroundJobManager.EnqueueAsync(new DischargeRoomArgs { Id = room.Id, ReservationId = reservation.Id }, delay: timeUntilExit);
            }
            else if (reservation.EntryDate > DateTime.Now)
            {
                TimeSpan timeUntilEntry = reservation.EntryDate - DateTime.Now;
                await _backgroundJobManager.EnqueueAsync(new FillRoomArgs { Id = room.Id, ReservationId = reservation.Id }, delay: timeUntilEntry);

                TimeSpan timeUntilExit = reservation.ExitDate - DateTime.Now;
                await _backgroundJobManager.EnqueueAsync(new DischargeRoomArgs { Id = room.Id, ReservationId = reservation.Id }, delay: timeUntilExit);
            }
        }
        private async Task RefreshCache(Reservation reservation, Func<List<GetReservationDto>, GetReservationDto, List<GetReservationDto>> action)
        {
            int hotelId = reservation.HotelId;
            var mapper = new GetReservationDtoMapper();
            var reservationDto = mapper.ToDto(reservation);
            var cacheKeys = new[]
            {
                (clientId: 0, roomId: 0),
                (clientId: reservation.ClientId, roomId: 0),
                (clientId: 0, roomId: reservation.RoomId),
                (clientId: reservation.ClientId, roomId: reservation.RoomId)
            };

            foreach (var (clientId, roomId) in cacheKeys)
            {
                var cacheKey = $"GetReservationOfHotel-{hotelId}-AndCLient-{clientId}-AndRoom-{roomId}";
                var reservations = await _reservationsDistributedCache.GetAsync(cacheKey);

                if (reservations != null)
                {
                    reservations = action(reservations, reservationDto);
                    await _reservationsDistributedCache.SetAsync(cacheKey, reservations);
                }
            }
        }
    }
}
