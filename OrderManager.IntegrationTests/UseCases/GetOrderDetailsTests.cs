using System.Net.Http.Json;
using FluentAssertions;
using OrderManager.Application.UseCases.GetOrderDetails;
using OrderManager.IntegrationTests.AutoFixture;
using Xunit;

namespace OrderManager.IntegrationTests.UseCases
{
    public class GetOrderDetailsTests
    {
        [Theory]
        [IntegrationHostInlineData(1, 1, 9)]
        public async Task GetOrderById_OrderFound_ReturnsOrderDetails(
            int id,
            decimal expectedDiscountAmount,
            decimal expectedTotalAmount,
            IntegrationTestHostBuilder integrationTestHostBuilder)
        {
            using var host = integrationTestHostBuilder();
            using var client = host.CreateClient();

            var orderDetails = await client.GetFromJsonAsync<OrderDetailsDto>($"orders/{id}/details");

            orderDetails.Should()
                .BeEquivalentTo(new OrderDetailsDto()
                {
                    OrderId = id,
                    DiscountAmount = expectedDiscountAmount,
                    TotalAmount = expectedTotalAmount
                });
        }
    }
}
