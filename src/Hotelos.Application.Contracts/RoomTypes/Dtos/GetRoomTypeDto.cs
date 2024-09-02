using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.RoomTypes.Dtos
{
    public sealed class GetRoomTypeDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
    }
}
