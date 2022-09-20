using FluentValidation;
using FluentValidation.Results;
using OrderManager.Domain.Errors;

namespace OrderManager.Application;

public static class ValidationHelper
{
    public static void ThrowWithError(Error error)
    {
        var failures = new List<ValidationFailure>()
        {
            new ValidationFailure(string.Empty, error.ErrorMessage)
            {
                ErrorCode = error.ErrorCode
            }
        };

        throw new ValidationException(failures);
    }
}
