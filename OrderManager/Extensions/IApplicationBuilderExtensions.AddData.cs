using OrderManager.Infrastructure.EntityFramework;
using OrderManager.Infrastructure.EntityFramework.Extensions;
using OrderManager.Web.Extensions;

namespace OrderManager.Web.Extensions
{
    public static partial class IApplicationBuilderExtensions
    {
        public static void AddData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<OrderManagerDbContext>();
            dbContext!.AddData();
        }
    }
}
