using FluentValidation;
using OrderManager.Domain.Errors;

namespace OrderManager.Application.Extensions
{
    public static class IRuleBuilderOptionsExtensions
    {
        public static IRuleBuilder<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, Error error)
        {
            return builder
                .WithMessage(error.ErrorMessage)
                .WithErrorCode(error.ErrorCode);
        }
    }
}
