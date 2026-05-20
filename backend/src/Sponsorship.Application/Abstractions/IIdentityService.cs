using Sponsorship.Application.Auth.DTOs;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Abstractions
{
    public interface IIdentityService
    {
        Task<Result<LoginResponse>> GetTokenAsync(string email, string password);
    }
}
