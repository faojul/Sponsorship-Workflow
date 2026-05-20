using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Abstractions
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        IList<string> Roles { get; }
    }
}
