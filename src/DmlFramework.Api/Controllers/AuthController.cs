using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DmlFramework.Application.Features.User.Commands;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Application.Features.User.Queries;
using DmlFramework.Application.Features.Auth.Models;
using DmlFramework.Application.Features.Auth.Queries;
using DmlFramework.Infrastructure.Results;

namespace DmlFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<LoginResponse>))]
        public async Task<IActionResult> PostAsync([FromBody] GetLoginQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
