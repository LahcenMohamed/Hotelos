using FluentValidation;
using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.RoomTypes;
using Hotelos.Application.Contracts.RoomTypes.Dtos;
using Hotelos.Application.RoomTypes.Mappers;
using Hotelos.Application.RoomTypes.Validators;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.RoomTypes
{
    [Authorize]
    public class RoomTypeService(IRepository<RoomType> roomTypeRepository,
                                 IDistributedCache<List<GetRoomTypeDto>> distributedCache) : BaseServices, IRoomTypeService
    {
        private readonly IRepository<RoomType> _roomTypeRepository = roomTypeRepository;
        private readonly IDistributedCache<List<GetRoomTypeDto>> _distributedCache = distributedCache;


        [Authorize(HotelosPermissions.CreateRoomType)]
        public async Task<GetRoomTypeDto> Create(CreateRoomTypeDto createRoomTypeDto)
        {
            var validateResult = new CreateRoomTypeDtoValidator().Validate(createRoomTypeDto);
            if (!validateResult.IsValid)
            {
                var message = ValidationErorrResult(validateResult);
                throw new ValidationException(message);
            }

            (var hotelId, var userId) = GetHotelIdAndUserId();

            RoomType roomType = RoomType.Create(createRoomTypeDto.Name,
                                                hotelId,
                                                userId);

            await _roomTypeRepository.InsertAsync(roomType, true);

            var mapper = new GetSingleRoomTypeMapper();
            var roomTypeMapping = mapper.ToDto(roomType);
            return roomTypeMapping;
        }

        [Authorize(HotelosPermissions.DeleteRoomType)]
        public async Task<string> Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var roomType = await _roomTypeRepository.FirstOrDefaultAsync(x => x.Id == id && x.HotelId == hotelId);

            if (roomType is null)
            {
                throw new EntityNotFoundException();
            }
            await _roomTypeRepository.DeleteAsync(roomType, true);
            return "Success";
        }

        [Authorize(HotelosPermissions.GetAllRoomType)]
        public async Task<List<GetRoomTypeDto>> GetAll()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var cachingResult = await _distributedCache.GetOrAddAsync($"GetAllRoomTypesOfUser_{hotelId}",
                                                             async () => await GetAllFromDb());
            return cachingResult;
        }

        [Authorize(HotelosPermissions.UpdateRoomType)]
        public async Task<GetRoomTypeDto> Update(UpdateRoomTypeDto updateRoomType)
        {
            var validateResult = new UpdateRoomTypeDtoValidator().Validate(updateRoomType);

            if (!validateResult.IsValid)
            {
                var message = ValidationErorrResult(validateResult);
                throw new ValidationException(message);
            }

            (var hotelId, var userId) = GetHotelIdAndUserId();

            var roomType = await _roomTypeRepository.FirstOrDefaultAsync(x => x.Id == updateRoomType.Id && x.HotelId == hotelId);
            if (roomType is null)
            {
                throw new EntityNotFoundException();
            }

            roomType.Update(updateRoomType.Name, userId);

            await _roomTypeRepository.UpdateAsync(roomType, true);
            var mapper = new GetSingleRoomTypeMapper();
            var roomTypeMapping = mapper.ToDto(roomType);

            return roomTypeMapping;
        }

        private async Task<List<GetRoomTypeDto>> GetAllFromDb()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var roomTypes = await _roomTypeRepository.GetQueryableAsync();
            return roomTypes.Where(x => x.HotelId == hotelId).ToDto().ToList();
        }
    }
}
