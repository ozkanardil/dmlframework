using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DmlFramework.Application.Features.OrderItem.Models;
using DmlFramework.Application.Features.OrderItem.Queries;
using DmlFramework.Infrastructure.Results;

namespace DmlFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<OrderItemResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetOrderItemQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
