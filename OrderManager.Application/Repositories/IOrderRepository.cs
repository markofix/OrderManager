using OrderManager.Domain.Entites;

namespace OrderManager.Application.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task<Order?> GetOrderById(int orderId);
    }
}
