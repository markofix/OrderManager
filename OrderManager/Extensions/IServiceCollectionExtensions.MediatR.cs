using MediatR;
using OrderManager.Application.PipelineBehaviors;
using OrderManager.Application.UseCases.CreateOrder;

namespace OrderManager.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateOrderRequest).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
