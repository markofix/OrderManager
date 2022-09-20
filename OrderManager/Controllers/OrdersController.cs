using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Responses;
using OrderManager.Application.UseCases.Checkout;
using OrderManager.Application.UseCases.CreateOrder;
using OrderManager.Application.UseCases.GetOrderDetails;

namespace OrderManager.Web.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ApiController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await Mediator.Send(request);

            if (!result.IsSuccessful)
            {
                return ReturnError(result.Error);
            }

            return Ok();
        }

        [HttpPost]
        [Route("{orderId:int}/checkout")]
        [ProducesResponseType(typeof(CheckoutResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromRoute] int orderId)
        {
            var request = new CheckoutRequest()
            {
                OrderId = orderId
            };

            var result = await Mediator.Send(request);

            if (!result.IsSuccessful)
            {
                return ReturnError(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{orderId:int}/details")]
        [ProducesResponseType(typeof(OrderDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetails([FromRoute] int orderId)
        {
            var request = new GetOrderDetailsRequest()
            {
                OrderId = orderId
            };

            var result = await Mediator.Send(request);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
