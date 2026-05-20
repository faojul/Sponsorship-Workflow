using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Auth.DTOs
{
    public class UserResponse
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public string? FullName { get; set; }
        public required string Email { get; set; }
        public required string DisplayName { get; set; }
        public string? RoleId { get; set; }
        public string? Role { get; set; }

    }
}
