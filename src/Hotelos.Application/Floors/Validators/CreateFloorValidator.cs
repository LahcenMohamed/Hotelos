using FluentValidation;
using Hotelos.Application.Contracts.Floors.Dtos;

namespace Hotelos.Application.Floors.Validators
{
    public class CreateFloorValidator : AbstractValidator<CreateFloorDto>
    {
        public CreateFloorValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);
        }
    }
}
