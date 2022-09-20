using Microsoft.EntityFrameworkCore;
using OrderManager.Application.UseCases.GetOrderDetails;

namespace OrderManager.Infrastructure.EntityFramework.Queries
{
    internal class GetOrderDetailsQuery : IGetOrderDetailsQuery
    {
        private readonly OrderManagerDbContext _dbContext;

        public GetOrderDetailsQuery(OrderManagerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<OrderDetailsDto?> Execute(int orderId)
        {
            return await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDetailsDto()
                {
                    OrderId = o.Id,
                    TotalAmount = o.TotalAmount,
                    DiscountAmount = o.DiscountAmount,
                })
                .SingleOrDefaultAsync();
        }
    }
}
