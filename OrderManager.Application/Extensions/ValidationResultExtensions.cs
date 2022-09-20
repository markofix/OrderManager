using FluentValidation.Results;
using OrderManager.Domain.Errors;

namespace OrderManager.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<Error> ToErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(x => new Error(x.ErrorCode, x.ErrorMessage));
        }
    }
}
