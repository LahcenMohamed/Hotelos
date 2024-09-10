using FluentValidation;
using Hotelos.Application.Contracts.Services.Dtos;

namespace Hotelos.Application.Services.Validators
{
    public sealed class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0m);
        }
    }
}
