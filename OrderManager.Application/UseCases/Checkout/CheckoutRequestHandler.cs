using MediatR;
using OrderManager.Application.Repositories;
using OrderManager.Domain.Dates;
using OrderManager.Domain.Entites;
using OrderManager.Domain.Errors;
using OrderManager.Domain.OperationResult;

namespace OrderManager.Application.UseCases.Checkout
{
    public class CheckoutRequestHandler : IRequestHandler<CheckoutRequest, Result<CheckoutResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CheckoutRequestHandler(IOrderRepository orderRepository, IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<Result<CheckoutResponse>> Handle(CheckoutRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order is null)
            {
                return Result<CheckoutResponse>.Failure(Errors.Order.OrderNotFound());
            }

            var result = order!.CheckoutOrder(_dateTimeProvider.UtcNow());

            if (!result.IsSuccessful)
            {
                return Result<CheckoutResponse>.Failure(result.Error);
            }

            await _orderRepository.UpdateOrder(order);
            return Result<CheckoutResponse>.Success(CreateOrderDetails(order));
        }

        private CheckoutResponse CreateOrderDetails(Order order)
        {
            return new CheckoutResponse()
            {
                OrderId = order.Id,
                DiscountAmount = order.DiscountAmount,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
