using Microsoft.AspNetCore.Http;
using Sponsorship.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sponsorship.Infrastructure.Identity
{
    public class CurrentUserService
    : ICurrentUserService
    {
        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor =
                httpContextAccessor;
        }

        public string UserId =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? string.Empty;

        public string Email =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.Email)
            ?? string.Empty;

        public bool IsAuthenticated =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .Identity?
                .IsAuthenticated
            ?? false;

        public IList<string> Roles =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList()
            ?? [];
    }
}
