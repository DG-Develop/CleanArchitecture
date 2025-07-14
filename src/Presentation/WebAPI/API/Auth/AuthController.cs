using ECommerce.Application.Features.AuthAggregate.Dtos;
using ECommerce.Application.Features.AuthAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.API.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _mediator.Send(new LoginQuery(request.Email, request.Password));
            return Ok(new { Token = token });
        }
    }
}
