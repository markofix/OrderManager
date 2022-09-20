using OrderManager.Domain.Entites;

namespace OrderManager.Domain.Services.PriceCalculator
{
    internal interface IPriceCalculator
    {
        decimal CalculateTotalAmount(IEnumerable<OrderItem> orderItems);
        decimal CalculateDiscountAmount(IEnumerable<OrderItem> orderItems);
    }
}
