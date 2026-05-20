using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Auth.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public IList<string> Roles { get; set; }
            = new List<string>();
    }
}
