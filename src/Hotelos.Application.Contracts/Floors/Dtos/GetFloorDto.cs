using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Floors.Dtos
{
    public sealed class GetFloorDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
    }
}
