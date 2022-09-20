using Microsoft.EntityFrameworkCore;
using OrderManager.Application.Repositories;
using OrderManager.Domain.Entites;
using OrderManager.Domain.ValueObjects;
using OrderManager.Infrastructure.EntityFramework.Factories;

namespace OrderManager.Infrastructure.EntityFramework.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly OrderManagerDbContext _dbContext;
        private readonly IProductFactory _productFactory;

        public OrderRepository(OrderManagerDbContext dbContext, IProductFactory productFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _productFactory = productFactory ?? throw new ArgumentNullException(nameof(productFactory));
        }

        public async Task CreateOrder(Order order)
        {
            var dbOrder = new Models.Order
            {
                TotalAmount = order.TotalAmount,
                DiscountAmount = order.DiscountAmount,
                CreatedOnUtc = order.CreatedOnUtc,
                CompletedOnUtc = order.CompletedOnUtc,
                OrderStatusId = (byte)order.OrderStatus,
                RestaurantId = order.Restaurant.Id,
                OrderItems = order.OrderItems
                    .Select(item => new Models.OrderItem()
                    {
                        OrderId = order.Id,
                        Amount = item.Amount,
                        DiscountAmount = item.DiscountAmount,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity.Value,
                    })
                    .ToList()
            };

            await _dbContext.Orders.AddAsync(dbOrder);
            await _dbContext.SaveChangesAsync();
            order.Id = dbOrder.Id;
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _dbContext.Orders
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.SpecialOffers)
                            .ThenInclude(x => x.SpecialOfferItems)
                                .ThenInclude(x => x.Product)
                .Select(order => new Order(
                    order.OrderItems
                        .Select(orderItem =>
                            new OrderItem(
                                _productFactory.BuildProductFromDbModel(orderItem.Product),
                                new Quantity(orderItem.Quantity),
                                orderItem.Amount,
                                orderItem.DiscountAmount)),
                    new Restaurant(order.Restaurant.Name, order.Restaurant.TimeZoneOffset),
                    order.CreatedOnUtc,
                    order.CompletedOnUtc,
                    (Domain.Enums.OrderStatus)order.OrderStatusId)
                {
                    Id = order.Id
                })
                .SingleOrDefaultAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            var dbOrder = await _dbContext.Orders
                .Where(x => x.Id == order.Id)
                .SingleAsync();

            dbOrder.TotalAmount = order.TotalAmount;
            dbOrder.DiscountAmount = order.DiscountAmount;
            dbOrder.OrderStatusId = (byte)order.OrderStatus;

            _dbContext.Orders.Update(dbOrder);
            await _dbContext.SaveChangesAsync();
        }
    }
}
