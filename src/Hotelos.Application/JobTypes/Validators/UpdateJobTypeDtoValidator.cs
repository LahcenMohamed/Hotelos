using FluentValidation;
using Hotelos.Application.Contracts.JobTypes.Dtos;

namespace Hotelos.Application.JobTypes.Validators
{
    public sealed class UpdateJobTypeDtoValidator : AbstractValidator<UpdateJobTypeDto>
    {
        public UpdateJobTypeDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
