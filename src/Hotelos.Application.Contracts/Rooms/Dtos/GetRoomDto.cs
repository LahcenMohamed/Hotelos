using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Rooms.Dtos
{
    public sealed class GetRoomDto : FullAuditedEntityDto<int>
    {
        public int Number { get; set; }
        public string? Name { get; set; }
        public int CountOfBeds { get; set; }
        public decimal PriceOfOneNight { get; set; }
        public bool IsVacant { get; set; }
        public string? Description { get; set; }
        public int FloorId { get; set; }
        public int RoomTypeId { get; set; }
    }
}
