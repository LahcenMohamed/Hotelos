using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Subscriptions.Dtos
{
    public sealed class GetSubscriptionDto : FullAuditedEntityDto<int>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int NumberOfMonths { get; set; }
    }
}
