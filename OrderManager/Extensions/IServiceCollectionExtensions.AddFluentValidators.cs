using FluentValidation;
using OrderManager.Application.UseCases.CreateOrder;

namespace OrderManager.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateOrderRequestValidator).Assembly);
        }
    }
}
