using MediatR;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Commands
{
    public record FinanceApproveCommand(
    Guid RequestId)
    : IRequest<IResult>;
}
