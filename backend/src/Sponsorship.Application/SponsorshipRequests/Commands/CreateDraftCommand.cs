using MediatR;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Commands
{
    public record CreateDraftCommand(
        string Title,
        string Department,
        Guid SponsorshipTypeId,
        string EventName,
        DateTime EventDate,
        decimal RequestedAmount,
        string Purpose,
        string? ExpectedBusinessBenefit,
        string? Remarks)
    : IRequest<Result<Guid>>;
}
