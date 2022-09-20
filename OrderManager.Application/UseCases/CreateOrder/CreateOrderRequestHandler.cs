using MediatR;
using OrderManager.Application.Repositories;
using OrderManager.Application.UseCases.CreateOrder.Extensions;
using OrderManager.Domain.Dates;
using OrderManager.Domain.Entites;
using OrderManager.Domain.Enums;
using OrderManager.Domain.Errors;
using OrderManager.Domain.OperationResult;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Application.UseCases.CreateOrder
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Result>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateOrderRequestHandler(
            IRestaurantRepository restaurantRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<Result> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(request.RestaurantId);
            var products = await _productRepository.GetProductsByIds(request.ProductIds());
            var specialOffers = await _productRepository.GetSpecialOffersByProductIds(request.ProductIds());

            if (restaurant is null)
            {
                return Result.Failure(Errors.Restaurant.RestaurantNotFound());
            }

            if (products.Count() != request.OrderItems.Count())
            {
                return Result.Failure(Errors.Product.ProductNotFound());
            }

            var orderItems = CreateOrderItems(request, products, specialOffers);
            var order = Order.Create(orderItems, restaurant!, _dateTimeProvider.UtcNow());

            await _orderRepository.CreateOrder(order);

            return Result.Success();
        }

        private IList<OrderItem> CreateOrderItems(CreateOrderRequest request, IEnumerable<Product> products, IEnumerable<SpecialOffer> specialOffers)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in request.OrderItems)
            {
                var product = products.Single(x => x.Id == item.ProductId);
                orderItems.Add(CreateOrderItem(specialOffers, product, new Quantity(item.Quantity)));
            }

            return orderItems;
        }

        private OrderItem CreateOrderItem(IEnumerable<SpecialOffer> specialOffers, Product product, Quantity quantity)
        {
            decimal price;
            decimal discountAmount = 0;
            if (product.ProductType == ProductType.SpecialOffer)
            {
                var specialOffer = specialOffers.Single(x => x.Id == product.Id);
                price = specialOffer.PriceWithDiscount;
                discountAmount = specialOffer.DiscountAmount;
            }
            else
            {
                price = product.Price;
            }

            return new OrderItem(product, quantity, price, discountAmount);
        }
    }
}
