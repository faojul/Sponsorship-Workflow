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
    public class ManagerRejectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<ManagerRejectCommand, IResult>
    {
        public async Task<IResult> Handle(ManagerRejectCommand request, CancellationToken cancellationToken)
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
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only requests pending manager approval can be rejected.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.Rejected;

            sponsorshipRequest.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Rejected By Manager",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .Rejected,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(
                cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Rejected Successfully.");
        }
    }
}
