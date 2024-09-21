using FluentValidation;
using Hotelos.Application.Contracts.JobTimes.Dtos;

namespace Hotelos.Application.JobTimes.Validators
{
    public sealed class CreateJobTimeDtoValidator : AbstractValidator<CreateJobTimeDto>
    {
        public CreateJobTimeDtoValidator()
        {
            RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        }
    }
}
