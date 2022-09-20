using OrderManager.Domain.Enums;
using OrderManager.Domain.OperationResult;
using OrderManager.Domain.Services.PriceCalculator.Factory;

namespace OrderManager.Domain.Entites
{
    public class Order
    {
        public Order(IEnumerable<OrderItem> orderItems, Restaurant restaurant, DateTime createdOnUtc, DateTime? completedOnUtc, OrderStatus orderStatus)
        {
            OrderItems = orderItems.ToList();
            Restaurant = restaurant;
            CreatedOnUtc = createdOnUtc;
            CompletedOnUtc = completedOnUtc;
            OrderStatus = orderStatus;
            TotalAmount = orderItems.Sum(x => x.Amount);
            DiscountAmount = orderItems.Sum(x => x.DiscountAmount);
        }

        private Order(IEnumerable<OrderItem> orderItems, Restaurant restaurant, DateTime createdOnUtc)
        {
            OrderItems = orderItems.ToList();
            Restaurant = restaurant;
            CreatedOnUtc = createdOnUtc;
            OrderStatus = OrderStatus.Pending;
            TotalAmount = orderItems.Sum(x => x.Amount);
            DiscountAmount = orderItems.Sum(x => x.DiscountAmount);
        }

        public static Order Create(IList<OrderItem> orderItems, Restaurant restaurant, DateTime createdOnUtc)
        {
            return new Order(orderItems, restaurant, createdOnUtc);
        }

        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; }
        public DateTime? CompletedOnUtc { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public OrderStatus OrderStatus { get; private set; }
        public Restaurant Restaurant { get; }

        public IList<OrderItem> OrderItems { get; }

        public void AddItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public Result CheckoutOrder(DateTime completedOnUtc)
        {
            if (OrderStatus == OrderStatus.Completed)
            {
                return Result.Failure(Errors.Errors.Order.OrderAlreadyCompleted());
            }

            OrderStatus = OrderStatus.Completed;
            CompletedOnUtc = completedOnUtc;
            CalculateFinalAmounts();

            return Result.Success();
        }

        private void CalculateFinalAmounts()
        {
            var restaurantLocalTime = CompletedOnUtc!.Value.AddHours(Restaurant.TimeZoneOffset);
            var priceCalculator = new PriceCalculatorFactory().CreatePriceCalculator(restaurantLocalTime);

            TotalAmount = priceCalculator.CalculateTotalAmount(OrderItems);
            DiscountAmount = priceCalculator.CalculateDiscountAmount(OrderItems);
        }
    }
}
