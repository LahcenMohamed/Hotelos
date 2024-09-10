using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Services.Dtos
{
    public sealed class GetServiceDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
