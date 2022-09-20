#nullable disable
using Microsoft.AspNetCore.Mvc;
using OrderManager.Domain.Errors;

namespace OrderManager.Application.Responses
{
    public class ErrorResponse : ProblemDetails
    {
        public Error Error { get; set; }
    }
}
