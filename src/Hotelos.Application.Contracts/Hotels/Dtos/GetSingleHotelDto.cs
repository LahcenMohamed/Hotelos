using System;
using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Hotels.Dtos
{
    public sealed class GetSingleHotelDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
