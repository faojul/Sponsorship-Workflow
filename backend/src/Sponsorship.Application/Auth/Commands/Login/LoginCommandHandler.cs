using MediatR;
using Microsoft.AspNetCore.Identity;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Authentication;
using Sponsorship.Application.Auth.DTOs;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IIdentityService identityService): IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)         
        {
            return await identityService.GetTokenAsync(request.Email, request.Password);
        }
    }
}
