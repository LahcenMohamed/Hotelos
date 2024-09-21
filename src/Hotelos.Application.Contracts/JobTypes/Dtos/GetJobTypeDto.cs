using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.JobTypes.Dtos
{
    public sealed class GetJobTypeDto : EntityDto<int>
    {
        public string Title { get; set; }
    }
}
