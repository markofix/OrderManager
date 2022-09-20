namespace OrderManager.Application.UseCases.GetOrderDetails
{
    public interface IGetOrderDetailsQuery
    {
        Task<OrderDetailsDto?> Execute(int orderId);
    }
}
