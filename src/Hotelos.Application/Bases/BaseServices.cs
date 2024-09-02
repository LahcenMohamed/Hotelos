using Hotelos.Application.Contracts.Results;
using System.Linq;
using Volo.Abp.Application.Services;

namespace Hotelos.Application.Bases
{
    public class BaseServices : ApplicationService
    {
        public Result<T> ValidationErorrResult<T>(FluentValidation.Results.ValidationResult validationResult)
        {
            var message = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            return Result.UnprocessableEntity<T>(message);
        }
    }
}
