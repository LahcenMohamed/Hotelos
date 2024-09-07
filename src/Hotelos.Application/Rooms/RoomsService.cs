using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Rooms;
using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Application.Exceptions;
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
using Volo.Abp.Domain.Entities;
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
            var validateResult = await new CreateRoomDtoValidator(_roomTypeRepository, _floorRepository).ValidateAsync(createRoomDto);
            if (!validateResult.IsValid)
            {
                var erorrs = ValidationErorrResult(validateResult);
                throw new UnprocessableEntityException(erorrs);
            }
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;

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
            await RefershCach(room.FloorId, room.RoomTypeId, hotelId);
            return roomMapping;
        }

        [Authorize(HotelosPermissions.DeleteRoom)]
        public async Task Delete(int roomId)
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;

            var room = await _roomRepository.FirstOrDefaultAsync(x => x.Id == roomId && x.HotelId == hotelId);
            if (room is null)
            {
                throw new EntityNotFoundException();
            }

            await _roomRepository.DeleteAsync(room, true);
            await RefershCach(room.FloorId, room.RoomTypeId, hotelId);
        }

        [Authorize(HotelosPermissions.GetRooms)]
        public async Task<List<GetRoomDto>> GetAll(GetRoomsRequestDto getRoomsRequestDto)
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            var rooms = await _roomDtoListdistributedCache.GetOrAddAsync($"GetRoomsOfFloor-{getRoomsRequestDto.FloorId}-AndGetRoomsOfRoomType-{getRoomsRequestDto.RoomTypeId}-andHotel-{hotelId}",
                async () => await GetRoomsFromDb(getRoomsRequestDto.FloorId, getRoomsRequestDto.RoomTypeId, hotelId));
            return rooms;
        }

        [Authorize(HotelosPermissions.UpdateRoom)]
        public async Task<GetRoomDto> Update(UpdateRoomDto updateRoomDto)
        {
            var validateResult = await new UpdateRoomDtoValidator(_roomTypeRepository, _floorRepository).ValidateAsync(updateRoomDto);
            if (!validateResult.IsValid)
            {
                var erorrs = ValidationErorrResult(validateResult);
                throw new UnprocessableEntityException(erorrs);
            }

            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;

            var room = await _roomRepository.FirstOrDefaultAsync(x => x.Id == updateRoomDto.Id && x.HotelId == hotelId);
            if (room is null)
            {
                throw new EntityNotFoundException();
            }

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
            await RefershCach(room.FloorId, room.RoomTypeId, hotelId);
            return roomMapping;
        }

        private async Task<List<GetRoomDto>> GetRoomsFromDb(int floorId, int roomTypeId, int hotelId)
        {
            var rooms = await _roomRepository.GetQueryableAsync();
            return rooms.Where(x => x.HotelId == hotelId &&
            ((x.RoomTypeId == roomTypeId || roomTypeId == 0) &&
            (x.FloorId == floorId || floorId == 0))).ToDto().ToList();
        }

        private async Task RefershCach(int floorId, int roomTypeId, int hotelId)
        {
            await _roomDtoListdistributedCache.RemoveManyAsync([$"GetRoomsOfFloor-{0}-AndGetRoomsOfRoomType-{0}-andHotel-{hotelId}",
                                                                 $"GetRoomsOfFloor-{floorId}-AndGetRoomsOfRoomType-{roomTypeId}-andHotel-{hotelId}"]);

            await _roomDtoListdistributedCache.GetOrAddAsync($"GetRoomsOfFloor-{0}-AndGetRoomsOfRoomType-{0}-andHotel-{hotelId}",
                                                            async () => await GetRoomsFromDb(0, 0, hotelId));

            await _roomDtoListdistributedCache.GetOrAddAsync($"GetRoomsOfFloor-{floorId}-AndGetRoomsOfRoomType-{roomTypeId}-andHotel-{hotelId}",
                                                            async () => await GetRoomsFromDb(floorId, roomTypeId, hotelId));
        }
    }
}
