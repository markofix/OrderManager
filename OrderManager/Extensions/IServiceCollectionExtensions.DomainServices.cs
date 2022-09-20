using OrderManager.Domain.Dates;
using OrderManager.Infrastructure.Dates;

namespace OrderManager.Extensions;

public static partial class IServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}
