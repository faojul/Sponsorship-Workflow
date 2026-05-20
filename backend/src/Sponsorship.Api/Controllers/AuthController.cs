using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sponsorship.Application.Abstractions.Authentication;
using Sponsorship.Application.Auth.Commands.Login;
using Sponsorship.Application.Auth.DTOs;
using Sponsorship.Infrastructure.Identity;

namespace Sponsorship.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(
        IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
        LoginRequest request)
        {
            var command = new LoginCommand(
                request.Email,
                request.Password);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        }
}
