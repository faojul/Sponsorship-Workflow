using MediatR;
using Microsoft.AspNetCore.Identity;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Authentication;
using Sponsorship.Application.Auth.DTOs;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public IdentityService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<Result<LoginResponse>> GetTokenAsync(string email, string password)
        {
            var user =
                await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return await Result<LoginResponse>.FailAsync((int)HttpStatusCode.Unauthorized, "Invalid User Name Or Password.");
            }

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user, password);
                            
            if (!passwordValid)
            {
                return await Result<LoginResponse>.FailAsync((int)HttpStatusCode.Unauthorized, "Invalid User Name Or Password.");
            }

            var roles =
                await _userManager.GetRolesAsync(user);

            var token =
                _jwtTokenGenerator.GenerateToken(
                    user.Id,
                    user.Email!,
                    roles);

            return await Result<LoginResponse>.SuccessAsync((int)HttpStatusCode.OK, 
                new LoginResponse
                {
                    Token = token,
                    Email = user.Email!,
                    Roles = roles
                });
        }
    }
}
