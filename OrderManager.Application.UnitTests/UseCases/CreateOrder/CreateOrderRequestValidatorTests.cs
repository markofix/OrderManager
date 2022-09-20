using FluentAssertions;
using FluentValidation.TestHelper;
using OrderManager.Application.UseCases.CreateOrder;
using OrderManager.Domain.Errors;
using Xunit;

namespace OrderManager.Application.UnitTests.UseCases.CreateOrder
{
    public class CreateOrderRequestValidatorTests
    {
        [Theory]
        [AutoMoqInlineData(0)]
        [AutoMoqInlineData(-1)]
        public async Task Quantity_LessThenOne_HasValidationError(
            int quantity,
            CreateOrderRequest request,
            CreateOrderRequestValidator sut)
        {
            request.OrderItems = new List<CreateOrderRequest.OrderItemDto>()
            {
                new CreateOrderRequest.OrderItemDto()
                {
                    Quantity = quantity,
                }
            };

            var result = await sut.TestValidateAsync(request);

            var error = result.Errors.First(x => x.PropertyName.StartsWith(nameof(request.OrderItems)));
            error.ErrorCode.Should().Be(Errors.Order.QuantityShouldBeGreaterThenZero().ErrorCode);
        }
    }
}
