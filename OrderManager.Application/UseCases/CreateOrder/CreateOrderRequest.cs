#nullable disable
using MediatR;
using OrderManager.Domain.OperationResult;

namespace OrderManager.Application.UseCases.CreateOrder
{
    public class CreateOrderRequest : IRequest<Result>
    {
        public int RestaurantId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public class OrderItemDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
