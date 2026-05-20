using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        public string Issuer { get; set; } = default!;

        public string Audience { get; set; } = default!;

        public string SecretKey { get; set; } = default!;

        public int ExpiryMinutes { get; set; }
    }
}
