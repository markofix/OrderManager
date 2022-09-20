using MediatR;
using OrderManager.Domain.OperationResult;

namespace OrderManager.Application.UseCases.Checkout
{
    public class CheckoutRequest : IRequest<Result<CheckoutResponse>>
    {
        public int OrderId { get; set; }
    }
}
