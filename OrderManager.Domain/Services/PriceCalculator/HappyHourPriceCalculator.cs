using OrderManager.Domain.Entites;

namespace OrderManager.Domain.Services.PriceCalculator
{
    internal class HappyHourPriceCalculator : IPriceCalculator
    {
        private const int HAPPY_HOUR_DISCOUNT_PERCENTAGE = 20;

        public decimal CalculateDiscountAmount(IEnumerable<OrderItem> orderItems)
        {
            var totalAmountWithoutDiscount = orderItems.Sum(x => x.Amount);
            var happyHourDiscount = totalAmountWithoutDiscount * HAPPY_HOUR_DISCOUNT_PERCENTAGE / 100;
            var discountAmount = orderItems.Sum(x => x.DiscountAmount);
            return discountAmount + happyHourDiscount;
        }

        public decimal CalculateTotalAmount(IEnumerable<OrderItem> orderItems)
        {
            var totalAmount = orderItems.Sum(x => x.Amount);
            var happyHourDiscount = totalAmount * HAPPY_HOUR_DISCOUNT_PERCENTAGE / 100;
            return totalAmount - happyHourDiscount;
        }
    }
}
