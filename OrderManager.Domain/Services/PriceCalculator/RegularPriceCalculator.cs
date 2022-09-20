using OrderManager.Domain.Entites;

namespace OrderManager.Domain.Services.PriceCalculator
{
    internal class RegularPriceCalculator : IPriceCalculator
    {
        public decimal CalculateDiscountAmount(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(x => x.DiscountAmount);
        }

        public decimal CalculateTotalAmount(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Sum(x => x.Amount);
        }
    }
}
