using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Rooms;
using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Application.Reservations.Mappers;
using Hotelos.Application.Rooms.Mappers;
using Hotelos.Application.Rooms.Validators;
using Hotelos.Domain.Rooms;
using Hotelos.Domain.Rooms.Entities.Floors;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Rooms
{
    public class RoomsService(IRepository<Room, int> roomRepository,
                             IRepository<RoomType, int> roomTypeRepository,
                             IRepository<Floor> floorRepository,
                             IDistributedCache<List<GetRoomDto>> roomDtoListdistributedCache) : BaseServices, IRoomService
    {
        private readonly IRepository<Room, int> _roomRepository = roomRepository;
        private readonly IRepository<RoomType, int> _roomTypeRepository = roomTypeRepository;
        private readonly IRepository<Floor> _floorRepository = floorRepository;
        private readonly IDistributedCache<List<GetRoomDto>> _roomDtoListdistributedCache = roomDtoListdistributedCache;

        [Authorize(HotelosPermissions.CreateRoom)]
        public async Task<GetRoomDto> Create(CreateRoomDto createRoomDto)
        {
            await ValidationErorrResult(new CreateRoomDtoValidator(_roomTypeRepository, _floorRepository), createRoomDto, true);
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var room = Room.Create(createRoomDto.Number,
                                   createRoomDto.CountOfBeds,
                                   createRoomDto.PriceOfOneNight,
                                   createRoomDto.FloorId,
                                   createRoomDto.RoomTypeId,
                                   hotelId,
                                   userId,
                                   createRoomDto.Name,
                                   createRoomDto.Description);

            await _roomRepository.InsertAsync(room, true);
            var mappers = new GetSingleRoomDtoMapper();
            var roomMapping = mappers.ToDto(room);
            await RefreshCache(room, (list, dto) =>
            {
                list.RemoveAll(r => r.Id == dto.Id);
                return list;
            });
            return roomMapping;
        }

        [Authorize(HotelosPermissions.DeleteRoom)]
        public async Task Delete(int roomId)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var room = await FindAggragateRootAsync(_roomRepository, roomId, hotelId, "Room");

            await _roomRepository.DeleteAsync(room, true);
            await RefreshCache(room, (list, dto) =>
            {
                list.RemoveAll(r => r.Id == dto.Id);
                return list;
            });
        }

        [Authorize(HotelosPermissions.GetRooms)]
        public async Task<List<GetRoomDto>> GetAll(GetRoomsRequestDto getRoomsRequestDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var rooms = await _roomDtoListdistributedCache.GetOrAddAsync($"GetRoomsOfFloor-{getRoomsRequestDto.FloorId}-AndGetRoomsOfRoomType-{getRoomsRequestDto.RoomTypeId}-andHotel-{hotelId}",
                async () => await GetRoomsFromDb(getRoomsRequestDto.FloorId, getRoomsRequestDto.RoomTypeId, hotelId));
            return rooms;
        }

        [Authorize(HotelosPermissions.UpdateRoom)]
        public async Task<GetRoomDto> Update(UpdateRoomDto updateRoomDto)
        {
            await ValidationErorrResult(new UpdateRoomDtoValidator(_roomTypeRepository, _floorRepository), updateRoomDto, true);

            (var hotelId, var userId) = GetHotelIdAndUserId();

            var room = await FindAggragateRootAsync(_roomRepository, updateRoomDto.Id, hotelId, "Room");

            room.Update(updateRoomDto.Number,
                        updateRoomDto.CountOfBeds,
                        updateRoomDto.PriceOfOneNight,
                        updateRoomDto.FloorId,
                        updateRoomDto.RoomTypeId,
                        updateRoomDto.IsVacant,
                        userId,
                        updateRoomDto.Name,
                        updateRoomDto.Description);

            await _roomRepository.UpdateAsync(room, true);
            var mappers = new GetSingleRoomDtoMapper();
            var roomMapping = mappers.ToDto(room);
            await RefreshCache(room, (list, dto) =>
            {
                var index = list.FindIndex(r => r.Id == dto.Id);
                if (index != -1)
                    list[index] = dto;
                return list;
            });
            return roomMapping;
        }

        private async Task<List<GetRoomDto>> GetRoomsFromDb(int floorId, int roomTypeId, int hotelId)
        {
            var rooms = await _roomRepository.GetQueryableAsync();
            return rooms.Where(x => x.HotelId == hotelId &&
            ((x.RoomTypeId == roomTypeId || roomTypeId == 0) &&
            (x.FloorId == floorId || floorId == 0))).ToDto().ToList();
        }

        private async Task RefreshCache(Room room, Func<List<GetRoomDto>, GetRoomDto, List<GetRoomDto>> action)
        {
            int hotelId = room.HotelId;
            var mapper = new GetSingleRoomDtoMapper();
            var roomDto = mapper.ToDto(room);
            var cacheKeys = new[]
            {
                (floorId: 0, roomTypeId: 0),
                (floorId: room.FloorId, roomTypeId: 0),
                (floorId: 0, roomTypeId: room.RoomTypeId),
                (floorId: room.FloorId, roomTypeId: room.RoomTypeId)
            };

            foreach (var (floorId, roomTypeId) in cacheKeys)
            {
                var cacheKey = $"GetRoomsOfFloor-{floorId}-AndGetRoomsOfRoomType-{roomTypeId}-andHotel-{hotelId}";
                var rooms = await _roomDtoListdistributedCache.GetAsync(cacheKey);

                if (rooms != null)
                {
                    rooms = action(rooms, roomDto);
                    await _roomDtoListdistributedCache.SetAsync(cacheKey, rooms);
                }
            }
        }
    }
}
