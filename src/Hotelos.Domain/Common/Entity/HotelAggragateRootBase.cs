using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Common.Entity
{
    public abstract class HotelAggragateRootBase : FullAuditedAggregateRoot<int>
    {
        public int HotelId { get; protected set; }
    }

    public abstract class HotelEntityBase : FullAuditedEntity<int>
    {
        public int HotelId { get; protected set; }
    }
}
