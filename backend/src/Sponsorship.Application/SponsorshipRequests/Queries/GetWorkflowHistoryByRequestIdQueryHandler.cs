using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public class GetWorkflowHistoryByRequestIdQueryHandler(IApplicationDbContext context, IIdentityService identityService) : IRequestHandler<GetWorkflowHistoryByRequestIdQuery, Result<List<WorkflowHistoryDto>>>
    {
        public async Task<Result<List<WorkflowHistoryDto>>> Handle(GetWorkflowHistoryByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var history = await context.WorkflowHistories
            .AsNoTracking()
            .Where(w => w.SponsorshipRequestId == request.SponsorshipRequestId)
            .OrderBy(w => w.PerformedAtUtc) // Show oldest modifications first to track chronological progress
            .Select(history => new WorkflowHistoryDto {
                Id = history.Id,
                SponsorshipRequestId = history.SponsorshipRequestId,
                PreviousStatus = history.PreviousStatus.ToString(),
                NewStatus = history.NewStatus.ToString(),
                Action = history.Action,
                PerformedByUserId = history.PerformedByUserId,
                PerformedByUserName = "",
                Remarks = history.Remarks,
                PerformedAtUtc = history.PerformedAtUtc
            })
            .ToListAsync(cancellationToken);

            if (history == null)
            {
                return await Result<List<WorkflowHistoryDto>>.FailAsync((int)HttpStatusCode.NotFound, "Workflow history not found.");
            }

            var userIds = history.Select(h => h.PerformedByUserId).Distinct();
            var userNames = await identityService.GetUserNamesByIdsAsync(userIds.ToList(), cancellationToken);

            // Map user IDs to names in the history records
            history.ForEach(h =>
            {
                if (userNames.TryGetValue(h.PerformedByUserId, out var userName))
                {
                    h.PerformedByUserName = userName; // Replace user ID with name for better readability
                }
                else
                {
                    h.PerformedByUserName = "User" 
                ; // Fallback if user name is not found
            }
            });

            return await Result<List<WorkflowHistoryDto>>.SuccessAsync((int) HttpStatusCode.OK, history, "List of workflow histories.");
    }
}
}
