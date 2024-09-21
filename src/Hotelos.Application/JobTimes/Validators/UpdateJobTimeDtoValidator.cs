using FluentValidation;
using Hotelos.Application.Contracts.JobTimes.Dtos;

namespace Hotelos.Application.JobTimes.Validators
{
    public sealed class UpdateJobTimeDtoValidator : AbstractValidator<UpdateJobTimeDto>
    {
        public UpdateJobTimeDtoValidator()
        {
            RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        }
    }
}
