using MediatR;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public record GetWorkflowHistoryByRequestIdQuery(
Guid SponsorshipRequestId)
: IRequest<Result<List<WorkflowHistoryDto>>>;
}
