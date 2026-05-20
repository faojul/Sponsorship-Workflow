using MediatR;
using Sponsorship.Application.Auth.DTOs;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password): IRequest<Result<LoginResponse>>;
}
