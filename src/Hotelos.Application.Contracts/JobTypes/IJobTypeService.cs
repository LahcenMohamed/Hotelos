using Hotelos.Application.Contracts.JobTypes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.JobTypes
{
    public interface IJobTypeService
    {
        Task<GetJobTypeDto> Create(CreateJobTypeDto createJobTypeDto);
        Task<GetJobTypeDto> Update(UpdateJobTypeDto updateJobTypeDto);
        Task Delete(int id);
        Task<List<GetJobTypeDto>> GetAll();
    }
}
