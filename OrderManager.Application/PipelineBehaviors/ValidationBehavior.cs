#nullable disable
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OrderManager.Application.Extensions;
using OrderManager.Domain.OperationResult;

namespace OrderManager.Application.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(e => e.ValidateAsync(context, cancellationToken)));

            var isFailure = results
                .Any(r => !r.IsValid);

            if (isFailure)
            {
                return GetFailureResponse(results);
            }

            return await next();
        }

        private TResponse GetFailureResponse(ValidationResult[] results)
        {
            var responseType = typeof(TResponse);
            var errors = results
                .SelectMany(r => r.ToErrors());

            var dataType = responseType.GetGenericArguments().FirstOrDefault();
            if (dataType is null)
            {
                return Result.Failure(errors.First()) as TResponse;
            }

            var invalidResponse = Activator.CreateInstance(
                                            type: responseType,
                                            args: errors) as TResponse;

            return invalidResponse;
        }
    }
}
