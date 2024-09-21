using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.JobTypes;
using Hotelos.Application.Contracts.JobTypes.Dtos;
using Hotelos.Application.JobTypes.Mappers;
using Hotelos.Application.JobTypes.Validators;
using Hotelos.Application.Reservations.Mappers;
using Hotelos.Domain.Employees.Entities.JobTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.JobTypes
{
    public class JobTypeService(IRepository<JobType> jobTypeRepository,
                                IDistributedCache<List<GetJobTypeDto>> jobTypesDistributedCache) : BaseServices, IJobTypeService
    {
        private readonly IRepository<JobType> _jobTypeRepository = jobTypeRepository;
        private readonly IDistributedCache<List<GetJobTypeDto>> _jobTypesDistributedCache = jobTypesDistributedCache;

        public async Task<GetJobTypeDto> Create(CreateJobTypeDto createJobTypeDto)
        {
            await ValidationErorrResult(new CreateJobTypeDtoValidator(), createJobTypeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobType = JobType.Create(createJobTypeDto.Title,
                                         hotelId,
                                         userId);
            await _jobTypeRepository.InsertAsync(jobType, true);
            var mapper = new GetJobTypeDtoMapper();
            var jobTypeMapping = mapper.ToDto(jobType);
            await RefreshCache(jobTypeMapping, (list, dto) => { list.Add(dto); return list; });
            return jobTypeMapping;
        }

        public async Task Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobType = await FindEntityAsync(_jobTypeRepository, id, hotelId, "JobType");
            await _jobTypeRepository.DeleteAsync(jobType, true);
            await RefreshCache(new GetJobTypeDto() { Id = jobType.Id }, (list, dto) =>
            {
                list.RemoveAll(r => r.Id == dto.Id);
                return list;
            });
        }

        public async Task<List<GetJobTypeDto>> GetAll()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobTypes = await _jobTypesDistributedCache.GetOrAddAsync($"JobTypesOfHotel-{hotelId}",
                                                                   async () => await GetFromDb());
            return jobTypes;
        }

        public async Task<GetJobTypeDto> Update(UpdateJobTypeDto updateJobTypeDto)
        {
            await ValidationErorrResult(new UpdateJobTypeDtoValidator(), updateJobTypeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobType = await FindEntityAsync(_jobTypeRepository, updateJobTypeDto.Id, hotelId, "JobType");
            jobType.Update(updateJobTypeDto.Title, userId);
            await _jobTypeRepository.UpdateAsync(jobType, true);
            var mapper = new GetJobTypeDtoMapper();
            var jobTypeMapping = mapper.ToDto(jobType);
            await RefreshCache(jobTypeMapping, (list, dto) =>
            {
                var index = list.FindIndex(r => r.Id == dto.Id);
                if (index != -1)
                    list[index] = dto;
                return list;
            });
            return jobTypeMapping;
        }

        private async Task<List<GetJobTypeDto>> GetFromDb()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobTypes = await _jobTypeRepository.GetQueryableAsync();
            return jobTypes.Where(x => x.HotelId == hotelId).ToDto().ToList();
        }

        private async Task RefreshCache(GetJobTypeDto getJobTypeDto, Func<List<GetJobTypeDto>, GetJobTypeDto, List<GetJobTypeDto>> action)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var cacheKey = $"JobTypesOfHotel-{hotelId}";
            var jobTypes = await _jobTypesDistributedCache.GetAsync(cacheKey);

            if (jobTypes != null)
            {
                jobTypes = action(jobTypes, getJobTypeDto);
                await _jobTypesDistributedCache.SetAsync(cacheKey, jobTypes);
            }
        }
    }
}
