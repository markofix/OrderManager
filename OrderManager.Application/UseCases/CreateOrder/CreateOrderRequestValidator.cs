using FluentValidation;
using OrderManager.Application.Extensions;
using OrderManager.Domain.Errors;

namespace OrderManager.Application.UseCases.CreateOrder
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleForEach(r => r.OrderItems)
                .ChildRules(items =>
                {
                    items.RuleFor(x => x.Quantity)
                        .GreaterThan(0)
                        .WithError(Errors.Order.QuantityShouldBeGreaterThenZero());
                });
        }
    }
}
