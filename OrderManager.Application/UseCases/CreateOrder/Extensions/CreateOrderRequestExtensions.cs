namespace OrderManager.Application.UseCases.CreateOrder.Extensions
{
    public static class CreateOrderRequestExtensions
    {
        public static IEnumerable<int> ProductIds(this CreateOrderRequest request)
        {
            return request.OrderItems.Select(x => x.ProductId);
        }
    }
}
