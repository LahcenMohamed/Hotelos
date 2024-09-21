using Hotelos.Application.Contracts.JobTimes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.JobTimes
{
    public interface IJobTimeService
    {
        Task<GetJobTimeDto> Create(CreateJobTimeDto createJobTimeDto);
        Task<GetJobTimeDto> Update(UpdateJobTimeDto updateJobTimeDto);
        Task Delete(int id);
        Task<List<GetJobTimeDto>> GetAll(int employeeId);
    }
}
