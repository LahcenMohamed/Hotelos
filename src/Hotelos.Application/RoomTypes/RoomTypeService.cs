using FluentValidation;
using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Results;
using Hotelos.Application.Contracts.RoomTypes;
using Hotelos.Application.Contracts.RoomTypes.Dtos;
using Hotelos.Application.RoomTypes.Mappers;
using Hotelos.Application.RoomTypes.Validators;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.RoomTypes
{
    [Authorize]
    public class RoomTypeService(IRepository<RoomType> roomTypeRepository,
                                 IDistributedCache<List<GetRoomTypeDto>> distributedCache) : BaseServices, IRoomTypeService
    {
        private readonly IRepository<RoomType> _roomTypeRepository = roomTypeRepository;
        private readonly IDistributedCache<List<GetRoomTypeDto>> _distributedCache = distributedCache;


        public async Task<Result<GetRoomTypeDto>> Create(CreateRoomTypeDto createRoomTypeDto)
        {
            var validateResult = new CreateRoomTypeDtoValidator().Validate(createRoomTypeDto);
            if (!validateResult.IsValid)
            {
                var result = ValidationErorrResult<GetRoomTypeDto>(validateResult);
                return result;
            }

            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;

            RoomType roomType = RoomType.Create(createRoomTypeDto.Name,
                                                hotelId,
                                                userId);

            await _roomTypeRepository.InsertAsync(roomType, true);

            var mapper = new GetSingleRoomTypeMapper();
            var roomTypeMapping = mapper.ToDto(roomType);
            return Result.Created(roomTypeMapping);
        }

        public async Task<Result<string>> Delete(int id)
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            var roomType = await _roomTypeRepository.FirstOrDefaultAsync(x => x.Id == id && x.HotelId == hotelId);
            if (roomType is null)
            {
                return Result.NotFound<string>(null);
            }

            await _roomTypeRepository.DeleteAsync(roomType, true);
            return Result.Deleted<string>();
        }

        public async Task<Result<List<GetRoomTypeDto>>> GetAll()
        {
            var cachingResult = await _distributedCache.GetOrAddAsync($"GetAllRoomTypesOfUser_{CurrentUser.Id}",
                                                             async () => await GetAllFromDb());
            return Result.Success(cachingResult);
        }

        public async Task<Result<GetRoomTypeDto>> Update(UpdateRoomTypeDto updateRoomType)
        {
            var validateResult = new UpdateRoomTypeDtoValidator().Validate(updateRoomType);

            if (!validateResult.IsValid)
            {
                var result = ValidationErorrResult<GetRoomTypeDto>(validateResult);
                return result;
            }

            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);

            var roomType = await _roomTypeRepository.FirstOrDefaultAsync(x => x.Id == updateRoomType.Id && x.HotelId == hotelId);
            if (roomType is null)
            {
                return Result.NotFound<GetRoomTypeDto>(null);
            }

            Guid userId = (Guid)CurrentUser.Id;
            roomType.Update(updateRoomType.Name, userId);

            await _roomTypeRepository.UpdateAsync(roomType, true);

            var mapper = new GetSingleRoomTypeMapper();
            var roomTypeMapping = mapper.ToDto(roomType);

            return Result.Success(roomTypeMapping);
        }

        private async Task<List<GetRoomTypeDto>> GetAllFromDb()
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);

            var roomTypes = await _roomTypeRepository.GetQueryableAsync();
            return roomTypes.Where(x => x.HotelId == hotelId).ToDto().ToList();
        }
    }
}
