namespace OrderManager.Domain.Errors
{
    public static class Errors
    {
        public static class Order
        {
            public static Error RestaurantIdRequired() => new("ORDER0001", "Restaurant id is required");
            public static Error QuantityShouldBeGreaterThenZero() => new("ORDER0002", "Quantity should be greater then zero");
            public static Error OrderNotFound() => new("ORDER0003", "Order not found");
            public static Error OrderAlreadyCompleted() => new("ORDER0004", "Order already completed");
        }

        public static class Restaurant
        {
            public static Error RestaurantNotFound() => new("REST0001", "Restaurant not found");
        }
        public static class Product
        {
            public static Error ProductNotFound() => new("PROD0001", "Product not found");
        }
    }
}
