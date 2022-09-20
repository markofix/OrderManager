using MediatR;

namespace OrderManager.Application.UseCases.GetOrderDetails
{
    public class GetOrderDetailsRequest : IRequest<OrderDetailsDto>
    {
        public int OrderId { get; set; }
    }
}
