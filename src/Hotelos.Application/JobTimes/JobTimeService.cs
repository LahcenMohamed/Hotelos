using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.JobTimes;
using Hotelos.Application.Contracts.JobTimes.Dtos;
using Hotelos.Application.JobTimes.Mappers;
using Hotelos.Application.JobTimes.Validators;
using Hotelos.Domain.Employees.Entities.JobTimes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.JobTimes
{
    public class JobTimeService(IRepository<JobTime> jobTimeRepository) : BaseServices, IJobTimeService
    {
        private readonly IRepository<JobTime> _jobTimeRepository = jobTimeRepository;

        public async Task<GetJobTimeDto> Create(CreateJobTimeDto createJobTimeDto)
        {
            await ValidationErorrResult(new CreateJobTimeDtoValidator(), createJobTimeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobTime = JobTime.Create(createJobTimeDto.StartTime,
                                         createJobTimeDto.EndTime,
                                         createJobTimeDto.Day,
                                         createJobTimeDto.EmployeeId,
                                         userId);
            await _jobTimeRepository.InsertAsync(jobTime, true);
            var mapper = new GetJobTimeMapper();
            return mapper.ToDto(jobTime);
        }

        public async Task Delete(int id)
        {
            var jobTime = await _jobTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (jobTime is null)
            {
                throw new EntityNotFoundException();
            }
            await _jobTimeRepository.DeleteAsync(jobTime, true);
        }

        public async Task<List<GetJobTimeDto>> GetAll(int employeeId)
        {
            var jobTimes = await _jobTimeRepository.GetQueryableAsync();
            return jobTimes.Where(x => x.EmployeeId == employeeId).ToDto().ToList();
        }

        public async Task<GetJobTimeDto> Update(UpdateJobTimeDto updateJobTimeDto)
        {
            await ValidationErorrResult(new UpdateJobTimeDtoValidator(), updateJobTimeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobTime = await _jobTimeRepository.FirstOrDefaultAsync(x => x.Id == updateJobTimeDto.Id);
            if (jobTime is null)
            {
                throw new EntityNotFoundException();
            }
            jobTime.Update(updateJobTimeDto.StartTime,
                           updateJobTimeDto.EndTime,
                           updateJobTimeDto.Day,
                           userId);
            await _jobTimeRepository.UpdateAsync(jobTime, true);
            var mapper = new GetJobTimeMapper();
            return mapper.ToDto(jobTime);
        }
    }
}
