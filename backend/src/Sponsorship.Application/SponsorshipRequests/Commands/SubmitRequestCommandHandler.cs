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
    public class SubmitRequestCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<SubmitRequestCommand, IResult>
    {
        public async Task<IResult> Handle(SubmitRequestCommand request, CancellationToken cancellationToken)
        {
            var sponsorshipRequest =
                await context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            if (sponsorshipRequest is null)
            {
                return await Result.FailAsync((int)HttpStatusCode.NotFound, "Request not found.");
            }

            if (sponsorshipRequest.RequestorId != currentUserService.UserId)
            {
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "You cannot submit this request.");
            }

            if (sponsorshipRequest.Status != SponsorshipRequestStatus.Draft)
            {
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only draft requests can be submitted.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.PendingManagerApproval;
            sponsorshipRequest.UpdatedAtUtc = DateTime.UtcNow;

            context.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Submitted",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .PendingManagerApproval,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(
                cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Submitted Successfully.");
        }
    }
}
