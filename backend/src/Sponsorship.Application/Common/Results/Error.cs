using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.Common.Results
{
    public record Error(
    string Code,
    string Message);
}
