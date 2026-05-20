using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public class GetWorkflowHistoryByRequestIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetWorkflowHistoryByRequestIdQuery, Result<List<WorkflowHistoryDto>>>
    {
        public async Task<Result<List<WorkflowHistoryDto>>> Handle(GetWorkflowHistoryByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var history = await context.WorkflowHistories
            .AsNoTracking()
            .Where(w => w.SponsorshipRequestId == request.SponsorshipRequestId)
            .OrderBy(w => w.PerformedAtUtc) // Show oldest modifications first to track chronological progress
            .Select(history => new WorkflowHistoryDto(
                history.Id,
                history.SponsorshipRequestId,
                history.PreviousStatus.ToString(),
                history.NewStatus.ToString(),
                history.Action,
                history.PerformedByUserId,
                history.Remarks,
                history.PerformedAtUtc
            ))
            .ToListAsync(cancellationToken);

            if (history == null)
            {
                return await Result<List<WorkflowHistoryDto>>.FailAsync((int)HttpStatusCode.NotFound,"Workflow history not found.");
            }

            return await Result<List<WorkflowHistoryDto>>.SuccessAsync((int)HttpStatusCode.OK,history, "List of workflow histories.");
        }
    }
}
