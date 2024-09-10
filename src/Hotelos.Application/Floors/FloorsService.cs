using FluentValidation;
using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Floors;
using Hotelos.Application.Contracts.Floors.Dtos;
using Hotelos.Application.Floors.Mappers;
using Hotelos.Application.Floors.Validators;
using Hotelos.Domain.Rooms.Entities.Floors;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Floors
{
    public class FloorsService(IRepository<Floor> floorRepository,
                               IDistributedCache<List<GetFloorDto>> floorDistributedCache) : BaseServices, IFloorsService
    {
        private readonly IRepository<Floor> _floorRepository = floorRepository;
        private readonly IDistributedCache<List<GetFloorDto>> _floorDistributedCache = floorDistributedCache;

        [Authorize(HotelosPermissions.CreateFloor)]
        public async Task<GetFloorDto> Create(CreateFloorDto createFloorDtos)
        {
            var validateResult = new CreateFloorValidator().Validate(createFloorDtos);
            if (!validateResult.IsValid)
            {
                var message = ValidationErorrResult(validateResult);
                throw new ValidationException(message);
            }

            (var hotelId, var userId) = GetHotelIdAndUserId();

            Floor floor = Floor.Create(createFloorDtos.Name,
                                       hotelId,
                                       userId);

            await _floorRepository.InsertAsync(floor, true);

            var mapper = new GetFloorDtoMapper();
            var floorMapping = mapper.ToDto(floor);
            return floorMapping;
        }

        [Authorize(HotelosPermissions.UpdateFloor)]
        public async Task<GetFloorDto> Update(UpdateFloorDto updateFloorDto)
        {
            var validateResult = new UpdateFloorDtoValidator().Validate(updateFloorDto);
            if (!validateResult.IsValid)
            {
                var message = ValidationErorrResult(validateResult);
                throw new ValidationException(message);
            }

            (var hotelId, var userId) = GetHotelIdAndUserId();

            var floor = await _floorRepository.FirstOrDefaultAsync(x => x.Id == updateFloorDto.Id && x.HotelId == hotelId);

            if (floor is null)
            {
                throw new EntityNotFoundException();
            }

            floor.Update(updateFloorDto.Name, userId);

            await _floorRepository.UpdateAsync(floor, true);

            var mapper = new GetFloorDtoMapper();
            var floorMapping = mapper.ToDto(floor);

            return floorMapping;
        }

        [Authorize(HotelosPermissions.DeleteFloor)]
        public async Task<string> Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var floor = await _floorRepository.FirstOrDefaultAsync(x => x.Id == id && x.HotelId == hotelId);
            if (floor is null)
            {
                throw new EntityNotFoundException();
            }

            await _floorRepository.DeleteAsync(floor, true);
            return "Success";
        }

        [Authorize(HotelosPermissions.GetAllFloors)]
        public async Task<List<GetFloorDto>> GetAll()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var cachingResult = await _floorDistributedCache.GetOrAddAsync($"GetAllFloorsOfUser_{hotelId}",
                                                            async () => await GetAllFromDb());
            return cachingResult;
        }

        private async Task<List<GetFloorDto>> GetAllFromDb()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var floors = await _floorRepository.GetQueryableAsync();
            return floors.Where(x => x.HotelId == hotelId).ToResult().ToList();
        }
    }
}
