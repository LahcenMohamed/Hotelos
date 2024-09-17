using FluentValidation;
using FluentValidation.Results;
using Hotelos.Application.Exceptions;
using Hotelos.Domain.Common.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Bases
{
    public class BaseServices : ApplicationService
    {
        protected async Task ValidationErorrResult<TDto, TValidator>(TValidator validator, TDto dto, bool isAsync = false)
            where TValidator : AbstractValidator<TDto>
        {
            ValidationResult validationResult;
            if (isAsync)
            {
                validationResult = await validator.ValidateAsync(dto);
            }
            else
            {
                validationResult = validator.Validate(dto);
            }
            if (!validationResult.IsValid)
            {
                var erorrs = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                throw new UnprocessableEntityException(erorrs);
            }
        }

        protected (int, Guid) GetHotelIdAndUserId()
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);
            Guid userId = (Guid)CurrentUser.Id;
            return (hotelId, userId);
        }

        protected async Task<T> FindAggragateRootAsync<T>(IRepository<T> repository, int entityId, int hotelId, string entityName) where T : HotelAggragateRootBase
        {
            var entity = await repository.FirstOrDefaultAsync(x => x.Id == entityId && x.HotelId == hotelId);
            if (entity == null)
            {
                throw new EntityNotFoundException($"{entityName} not found");
            }
            return entity;
        }

        protected async Task<T> FindEntityAsync<T>(IRepository<T> repository, int entityId, int hotelId, string entityName) where T : HotelEntityBase
        {
            var entity = await repository.FirstOrDefaultAsync(x => x.Id == entityId && x.HotelId == hotelId);
            if (entity == null)
            {
                throw new EntityNotFoundException($"{entityName} not found");
            }
            return entity;
        }
    }
}
