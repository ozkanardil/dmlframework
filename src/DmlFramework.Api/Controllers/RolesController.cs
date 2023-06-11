using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DmlFramework.Application.Features.User.Models;
using DmlFramework.Application.Features.User.Queries;
using System.Data;
using DmlFramework.Application.Features.Role.Models;
using DmlFramework.Application.Features.Role.Queries;
using DmlFramework.Infrastructure.Results;

namespace DmlFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<RoleResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetRoleQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
