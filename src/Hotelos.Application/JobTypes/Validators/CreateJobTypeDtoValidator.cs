using FluentValidation;
using Hotelos.Application.Contracts.JobTypes.Dtos;

namespace Hotelos.Application.JobTypes.Validators
{
    public sealed class CreateJobTypeDtoValidator : AbstractValidator<CreateJobTypeDto>
    {
        public CreateJobTypeDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
