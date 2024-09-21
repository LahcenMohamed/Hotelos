using FluentValidation;
using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Services;
using Hotelos.Application.Contracts.Services.Dtos;
using Hotelos.Application.Services.Mappers;
using Hotelos.Application.Services.Validators;
using Hotelos.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Services
{
    public class ServicesService(IRepository<Service, int> serviceRepository,
                                 IDistributedCache<List<GetServiceDto>> serviceDistributedCache) : BaseServices, IServicesService
    {
        private readonly IRepository<Service, int> _serviceRepository = serviceRepository;
        private readonly IDistributedCache<List<GetServiceDto>> _serviceDistributedCache = serviceDistributedCache;

        public async Task<GetServiceDto> Create(CreateServiceDto createServiceDto)
        {
            await ValidationErorrResult(new CreateServiceDtoValidator(), createServiceDto);

            (var hotelId, var userId) = GetHotelIdAndUserId();
            var service = Service.Create(createServiceDto.Name,
                                         createServiceDto.Price,
                                         userId,
                                         hotelId,
                                         createServiceDto.Description);

            await _serviceRepository.InsertAsync(service, true);
            //await Refersh();
            var mapper = new GetServiceDtoMapper();
            return mapper.ToDto(service);
        }

        public async Task Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var service = await FindAggragateRootAsync(_serviceRepository, id, hotelId, "Service");

            await _serviceRepository.DeleteAsync(service, true);
            //await Refersh();
        }

        public async Task<List<GetServiceDto>> GetAll()
        {
            //(var hotelId, var userId) = GetHotelIdAndUserId();
            //var service = await _serviceDistributedCache.GetOrAddAsync($"GetAllServiceByHotelId-{hotelId}",
            //                                             async () => await GetAllFromDb());
            //return service;
            return await GetAllFromDb();
        }

        public async Task<GetServiceDto> Update(UpdateServiceDto updateServiceDto)
        {
            await ValidationErorrResult(new UpdateServiceDtoValidator(), updateServiceDto);

            (var hotelId, var userId) = GetHotelIdAndUserId();
            var service = await FindAggragateRootAsync(_serviceRepository, updateServiceDto.Id, hotelId, "Service");
            service.Update(updateServiceDto.Name,
                           updateServiceDto.Price,
                           updateServiceDto.Description,
                           userId);
            await _serviceRepository.UpdateAsync(service, true);
            //await Refersh();
            var mapper = new GetServiceDtoMapper();
            return mapper.ToDto(service);
        }

        private async Task<List<GetServiceDto>> GetAllFromDb()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var service = await _serviceRepository.GetQueryableAsync();
            return service.Where(x => x.HotelId == hotelId).ToProjection().ToList();
        }

        private async Task Refersh()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            await _serviceDistributedCache.RemoveAsync($"GetAllServiceByHotelId-{hotelId}");
            await _serviceDistributedCache.GetOrAddAsync($"GetAllServiceByHotelId-{hotelId}",
                                                         async () => await GetAllFromDb());
        }
    }
}
