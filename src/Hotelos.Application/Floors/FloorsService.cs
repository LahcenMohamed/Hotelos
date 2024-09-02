using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Floors;
using Hotelos.Application.Contracts.Floors.Dtos;
using Hotelos.Application.Contracts.Results;
using Hotelos.Application.Floors.Mappers;
using Hotelos.Application.Floors.Validators;
using Hotelos.Domain.Rooms.Entities.Floors;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Floors
{
    [Authorize]
    public class FloorsService(IRepository<Floor> floorRepository,
                               IDistributedCache<List<GetFloorDto>> floorDistributedCache) : BaseServices, IFloorsService
    {
        private readonly IRepository<Floor> _floorRepository = floorRepository;
        private readonly IDistributedCache<List<GetFloorDto>> _floorDistributedCache = floorDistributedCache;

        public async Task<Result<GetFloorDto>> Create(CreateFloorDto createFloorDtos)
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);

            var validateResult = new CreateFloorValidator().Validate(createFloorDtos);
            if (!validateResult.IsValid)
            {
                var result = ValidationErorrResult<GetFloorDto>(validateResult);
                return result;
            }

            Guid userId = (Guid)CurrentUser.Id;
            Floor floor = Floor.Create(createFloorDtos.Name,
                                       hotelId,
                                       userId);

            await _floorRepository.InsertAsync(floor, true);

            var mapper = new GetFloorDtoMapper();
            var floorMapping = mapper.ToDto(floor);
            return Result.Created(floorMapping);
        }

        public async Task<Result<GetFloorDto>> Update(UpdateFloorDto updateFloorDto)
        {
            var validateResult = new UpdateFloorDtoValidator().Validate(updateFloorDto);
            if (!validateResult.IsValid)
            {
                var result = ValidationErorrResult<GetFloorDto>(validateResult);
                return result;
            }

            var floor = await _floorRepository.FirstOrDefaultAsync(x => x.Id == updateFloorDto.Id);

            Guid userId = (Guid)CurrentUser.Id;
            floor.Update(updateFloorDto.Name, userId);

            await _floorRepository.UpdateAsync(floor, true);

            var mapper = new GetFloorDtoMapper();
            var floorMapping = mapper.ToDto(floor);

            return Result.Success(floorMapping);
        }

        public async Task<Result<string>> Delete(int id)
        {
            var floor = await _floorRepository.FirstOrDefaultAsync(x => x.Id == id);
            await _floorRepository.DeleteAsync(floor, true);
            return Result.Deleted<string>();
        }

        public async Task<Result<List<GetFloorDto>>> GetAll()
        {
            var cachingResult = await _floorDistributedCache.GetOrAddAsync($"GetAllFloorsOfUser_{CurrentUser.Id}",
                                                            async () => await GetAllFromDb());
            return Result.Success(cachingResult);
        }

        private async Task<List<GetFloorDto>> GetAllFromDb()
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);

            var floors = await _floorRepository.GetQueryableAsync();
            return floors.Where(x => x.HotelId == hotelId).ToResult().ToList();
        }
    }
}
