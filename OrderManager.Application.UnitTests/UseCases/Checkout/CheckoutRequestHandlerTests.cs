using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OrderManager.Application.Repositories;
using OrderManager.Application.UseCases.Checkout;
using OrderManager.Domain.Dates;
using OrderManager.Domain.Entites;
using OrderManager.Domain.Errors;
using Xunit;

namespace OrderManager.Application.UnitTests.UseCases.Checkout
{
    public class CheckoutRequestHandlerTests
    {
        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ValidRequest_CreateOrder(
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            Order order,
            CheckoutRequest request,
            CheckoutRequestHandler sut)
        {
            order.Id = request.OrderId;
            orderRepositoryMock
                .Setup(x => x.GetOrderById(request.OrderId))
                .ReturnsAsync(order);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();
            result.Data.TotalAmount.Should().Be(order.TotalAmount);
            result.Data.DiscountAmount.Should().Be(order.DiscountAmount);
            result.Data.OrderId.Should().Be(order.Id);

            order.OrderStatus.Should().Be(Domain.Enums.OrderStatus.Completed);

            orderRepositoryMock
                .Verify(x => x.UpdateOrder(order), Times.Once);
        }

        [Theory]
        [AutoMoqInlineData(20, 13, 00, 00)]
        [AutoMoqInlineData(20, 14, 59, 59)]
        public async Task Handle_ValidRequestHappyHour_CreateOrder(
            int happyHourDiscountPercentage,
            int hour,
            int minute,
            int second,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            [Frozen] Mock<IDateTimeProvider> dateTimeProviderMock,
            OrderItem orderItem,
            DateTime createdOnUtc,
            CheckoutRequest request,
            CheckoutRequestHandler sut)
        {
            var order = new Order(new List<OrderItem>() { orderItem }, new Restaurant("Restaurant 1", 0), createdOnUtc, null, Domain.Enums.OrderStatus.Pending)
            {
                Id = request.OrderId
            };
            orderRepositoryMock
                .Setup(x => x.GetOrderById(request.OrderId))
                .ReturnsAsync(order);

            var orderTotalAmount = order.TotalAmount;

            dateTimeProviderMock
                .Setup(x => x.UtcNow())
                .Returns(new DateTime(2022, 12, 12, hour, minute, second));

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();
            result.Data.TotalAmount.Should().Be(order.TotalAmount);
            result.Data.DiscountAmount.Should().Be(order.DiscountAmount);
            result.Data.OrderId.Should().Be(order.Id);

            order.TotalAmount.Should().Be(orderTotalAmount - (orderTotalAmount * happyHourDiscountPercentage / 100));
            order.OrderStatus.Should().Be(Domain.Enums.OrderStatus.Completed);

            orderRepositoryMock
                .Verify(x => x.UpdateOrder(order), Times.Once);
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_OrderAlreadyCompleted_ReturnsFailure(
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            Order order,
            DateTime now,
            CheckoutRequest request,
            CheckoutRequestHandler sut)
        {
            order.Id = request.OrderId;
            orderRepositoryMock
                .Setup(x => x.GetOrderById(request.OrderId))
                .ReturnsAsync(order);

            order.CheckoutOrder(now);
            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();
            result.Error.ErrorCode.Should().Be(Errors.Order.OrderAlreadyCompleted().ErrorCode);

            orderRepositoryMock
                .Verify(x => x.UpdateOrder(It.IsAny<Order>()), Times.Never);
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_OrderIsNull_ReturnsFailure(
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            CheckoutRequest request,
            CheckoutRequestHandler sut)
        {
            orderRepositoryMock
                .Setup(x => x.GetOrderById(request.OrderId))
                .ReturnsAsync((Order?)null);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();
            result.Error.ErrorCode.Should().Be(Errors.Order.OrderNotFound().ErrorCode);
            orderRepositoryMock
                .Verify(x => x.UpdateOrder(It.IsAny<Order>()), Times.Never);
        }
    }
}
