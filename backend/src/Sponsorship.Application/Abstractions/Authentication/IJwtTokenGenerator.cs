using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
            string userId,
            string email,
            IList<string> roles);
    }
}
