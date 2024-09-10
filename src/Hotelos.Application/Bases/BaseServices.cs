using System;
using System.Linq;
using Volo.Abp.Application.Services;

namespace Hotelos.Application.Bases
{
    public class BaseServices : ApplicationService
    {
        protected string ValidationErorrResult(FluentValidation.Results.ValidationResult validationResult)
        {
            var message = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            return message;
        }

        protected (int, Guid) GetHotelIdAndUserId()
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;
            return (hotelId, userId);
        }
    }
}
