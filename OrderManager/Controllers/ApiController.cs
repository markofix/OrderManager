using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Responses;
using OrderManager.Domain.Errors;

namespace OrderManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected ApiController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected IActionResult ReturnError(Error error)
        {
            var response = new ErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Error = error
            };

            return BadRequest(response);
        }
    }
}
