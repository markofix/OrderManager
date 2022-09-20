using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Infrastructure.EntityFramework.Factories;

namespace OrderManager.Infrastructure.EntityFramework.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services)
        {
            RegisterDbContext(services);
            RegisterRepositories(services);
            RegisterQueries(services);
            RegisterFactories(services);
        }

        private static void RegisterDbContext(IServiceCollection services)
        {
            services.AddDbContext<OrderManagerDbContext>(options => options
               .UseInMemoryDatabase("OrderManager")
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
                .AsMatchingInterface()
                .WithScopedLifetime());
        }

        private static void RegisterQueries(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Query")))
                .AsMatchingInterface()
                .WithScopedLifetime());
        }

        private static void RegisterFactories(IServiceCollection services)
        {
            services.AddScoped<IProductFactory, ProductFactory>();
        }
    }
}
