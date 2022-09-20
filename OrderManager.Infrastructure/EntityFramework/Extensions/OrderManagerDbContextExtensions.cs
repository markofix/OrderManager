using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Extensions
{
    public static class OrderManagerDbContextExtensions
    {
        public static void AddData(this OrderManagerDbContext dbContext)
        {
            AddRestaurants(dbContext);
            AddProductTypes(dbContext);
            AddOrderStatuses(dbContext);
            AddProducts(dbContext);
            AddSpecialOffers(dbContext);
            AddSpecialOfferItems(dbContext);
            AddOrders(dbContext);

            dbContext.SaveChanges();
        }

        private static void AddRestaurants(OrderManagerDbContext dbContext)
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant() { Id = 1, Name = "Restaurant 1", TimeZoneOffset = 11 },
                new Restaurant() { Id = 2, Name = "Restaurant 2", TimeZoneOffset = -8 },
                new Restaurant() { Id = 3, Name = "Restaurant 3", TimeZoneOffset = -3 }
            };

            dbContext.Restaurants.AddRange(restaurants);
        }

        private static void AddProductTypes(OrderManagerDbContext dbContext)
        {
            var productTypes = new List<ProductType>
            {
                new ProductType() { Id = (byte)Domain.Enums.ProductType.MainDish, Name = "Main dish"},
                new ProductType() { Id = (byte)Domain.Enums.ProductType.Drink, Name = "Drink"},
                new ProductType() { Id = (byte)Domain.Enums.ProductType.Dessert, Name = "Dessert"},
                new ProductType() { Id = (byte)Domain.Enums.ProductType.SpecialOffer, Name = "Special offer"}
            };

            dbContext.ProductTypes.AddRange(productTypes);
        }

        private static void AddOrderStatuses(OrderManagerDbContext dbContext)
        {
            var orderStatuses = new List<OrderStatus>
            {
                new OrderStatus() { Id = (byte)Domain.Enums.OrderStatus.Pending, Name = "Pending"},
                new OrderStatus() { Id = (byte)Domain.Enums.OrderStatus.Completed, Name = "Completed"}
            };

            dbContext.OrderStatuses.AddRange(orderStatuses);
        }

        private static void AddProducts(OrderManagerDbContext dbContext)
        {
            var products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Pizza", Price = 10, ProductTypeId = (byte)Domain.Enums.ProductType.MainDish },
                new Product() { Id = 2, Name = "Juice", Price = 5, ProductTypeId = (byte)Domain.Enums.ProductType.Drink },
                new Product() { Id = 3, Name = "Cake", Price = 5, ProductTypeId = (byte)Domain.Enums.ProductType.Dessert },
                new Product() { Id = 4, Name = "Happy meal", Price = 18, ProductTypeId = (byte)Domain.Enums.ProductType.SpecialOffer }
            };

            dbContext.Products.AddRange(products);
        }

        private static void AddSpecialOffers(OrderManagerDbContext dbContext)
        {
            var specialOffers = new List<SpecialOffer>()
            {
                new SpecialOffer() { ProductId = 4, DiscountPercentage = 10 }
            };

            dbContext.SpecialOffers.AddRange(specialOffers);
        }

        private static void AddSpecialOfferItems(OrderManagerDbContext dbContext)
        {
            var specialOfferProducts = new List<SpecialOfferItem>
            {
                new SpecialOfferItem() {ProductId = 1, SpecialOfferId = 4},
                new SpecialOfferItem() {ProductId = 2, SpecialOfferId = 4},
                new SpecialOfferItem() {ProductId = 3, SpecialOfferId = 4}
            };

            dbContext.SpecialOfferItems.AddRange(specialOfferProducts);
        }

        private static void AddOrders(OrderManagerDbContext dbContext)
        {
            var orders = new List<Order>
            {
                new Order() { Id = 1, CreatedOnUtc = DateTime.Now, DiscountAmount = 1, TotalAmount = 9, OrderStatusId = (byte)Domain.Enums.OrderStatus.Pending, RestaurantId = 2,
                    OrderItems = new List<OrderItem>() { new OrderItem() {Id =1, Amount = 9, DiscountAmount = 1, OrderId = 1, ProductId = 1, Quantity = 1 } } },
            };

            dbContext.Orders.AddRange(orders);
        }
    }
}
