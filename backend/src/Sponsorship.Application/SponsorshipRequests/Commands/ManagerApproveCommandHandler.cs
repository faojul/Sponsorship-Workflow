using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Domain.Entities;
using Sponsorship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net;

namespace Sponsorship.Application.SponsorshipRequests.Commands
{
    public class ManagerApproveCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<ManagerApproveCommand, IResult>
    {
        public async Task<IResult> Handle(ManagerApproveCommand request, CancellationToken cancellationToken)
        {
            var sponsorshipRequest =
                await context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            if (sponsorshipRequest is null)
            {
                return await Result.FailAsync((int)HttpStatusCode.NotFound, "Request not found.");
            }

            if (sponsorshipRequest.Status != SponsorshipRequestStatus.PendingManagerApproval)
            {
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only requests pending manager approval can be approved.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.PendingFinanceReview;
            sponsorshipRequest.UpdatedAtUtc = DateTime.UtcNow;

            context.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Approved By Manager",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .PendingFinanceReview,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(
                cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Approved Successfully.");
        }
    }
}
