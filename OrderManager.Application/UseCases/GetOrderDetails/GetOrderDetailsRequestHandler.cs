using MediatR;

namespace OrderManager.Application.UseCases.GetOrderDetails
{
    public class GetOrderDetailsRequestHandler : IRequestHandler<GetOrderDetailsRequest, OrderDetailsDto?>
    {
        private readonly IGetOrderDetailsQuery _query;

        public GetOrderDetailsRequestHandler(IGetOrderDetailsQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }
        public async Task<OrderDetailsDto?> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
        {
            var orderDetails = await _query.Execute(request.OrderId);
            return orderDetails;
        }
    }
}
