using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Infrastructure.Results;

namespace DmlFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<UserResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetUserByEmailQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(IRequestResult))]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IRequestResult))]
        public async Task<IActionResult> DeleteAsync(DeleteUserCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
