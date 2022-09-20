using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OrderManager.Application.Repositories;
using OrderManager.Application.UseCases.CreateOrder;
using OrderManager.Application.UseCases.CreateOrder.Extensions;
using OrderManager.Domain.Dates;
using OrderManager.Domain.Entites;
using OrderManager.Domain.Errors;
using Xunit;

namespace OrderManager.Application.UnitTests.UseCases.CreateOrder
{
    public class CreateOrderRequestHandlerTests
    {
        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ValidRequest_CreateOrder(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            [Frozen] Mock<IRestaurantRepository> restaurantRepositoryMock,
            [Frozen] Mock<IDateTimeProvider> dateTimeProviderMock,
            int specialOfferId,
            Restaurant restaurant,
            MainDish mainDish,
            MainDish mainDishSpecialOffer,
            Dessert dessertSpecialOffer,
            Drink drinkSpecialOffer,
            DateTime now,
            CreateOrderRequest request,
            CreateOrderRequestHandler sut)
        {
            request.OrderItems = new List<CreateOrderRequest.OrderItemDto>()
            {
                new CreateOrderRequest.OrderItemDto()
                {
                    ProductId = mainDish.Id,
                    Quantity = 1,
                },
                new CreateOrderRequest.OrderItemDto()
                {
                    ProductId = specialOfferId,
                    Quantity = 1,
                }
            };

            var specialOfferProducts = new List<Product>()
            {
                mainDishSpecialOffer,
                dessertSpecialOffer,
                drinkSpecialOffer
            };
            var specialOffer = new SpecialOffer(specialOfferProducts, "Promo", 10)
            {
                Id = specialOfferId
            };
            var products = new List<Product>()
            {
                mainDish,
                specialOffer
            };

            productRepositoryMock
                .Setup(x => x.GetProductsByIds(request.ProductIds()))
                .ReturnsAsync(products);

            productRepositoryMock
                .Setup(x => x.GetSpecialOffersByProductIds(request.ProductIds()))
                .ReturnsAsync(new List<SpecialOffer>() { specialOffer });

            restaurantRepositoryMock
                .Setup(x => x.GetRestaurantById(request.RestaurantId))
                .ReturnsAsync(restaurant);

            dateTimeProviderMock
                .Setup(x => x.UtcNow())
                .Returns(now);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();
            orderRepositoryMock
                .Verify(x =>
                    x.CreateOrder(It.Is<Order>(order =>
                        order.OrderStatus == Domain.Enums.OrderStatus.Pending &&
                        order.TotalAmount == specialOffer.PriceWithDiscount + mainDish.Price &&
                        order.DiscountAmount == specialOffer.DiscountAmount &&
                        order.CreatedOnUtc == now &&
                        order.Restaurant == restaurant &&
                        order.OrderItems.All(item => products.Contains(item.Product)))),
                Times.Once);
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_RestaurantIsNull_ReturnsFailure(
            [Frozen] Mock<IRestaurantRepository> restaurantRepositoryMock,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,

            CreateOrderRequest request,
            CreateOrderRequestHandler sut)
        {
            restaurantRepositoryMock
                .Setup(x => x.GetRestaurantById(request.RestaurantId))
                .ReturnsAsync((Restaurant?)null);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();
            result.Error.ErrorCode.Should().Be(Errors.Restaurant.RestaurantNotFound().ErrorCode);
            orderRepositoryMock
                .Verify(x => x.CreateOrder(It.IsAny<Order>()), Times.Never);

        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_SomeProductsNotFound_ReturnsFailure(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            MainDish mainDish,
            int specialOfferId,
            CreateOrderRequest request,
            CreateOrderRequestHandler sut)
        {
            request.OrderItems = new List<CreateOrderRequest.OrderItemDto>()
            {
                new CreateOrderRequest.OrderItemDto()
                {
                    ProductId = mainDish.Id,
                    Quantity = 1,
                },
                new CreateOrderRequest.OrderItemDto()
                {
                    ProductId = specialOfferId,
                    Quantity = 1,
                }
            };

            var products = new List<Product>()
            {
                mainDish
            };

            productRepositoryMock
                .Setup(x => x.GetProductsByIds(request.ProductIds()))
                .ReturnsAsync(products);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();
            result.Error.ErrorCode.Should().Be(Errors.Product.ProductNotFound().ErrorCode);
            orderRepositoryMock
                .Verify(x => x.CreateOrder(It.IsAny<Order>()), Times.Never);
        }
    }
}
