using FluentValidation;
using Hotelos.Application.Contracts.Floors.Dtos;

namespace Hotelos.Application.Floors.Validators
{
    public sealed class UpdateFloorDtoValidator : AbstractValidator<UpdateFloorDto>
    {
        public UpdateFloorDtoValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);

            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
