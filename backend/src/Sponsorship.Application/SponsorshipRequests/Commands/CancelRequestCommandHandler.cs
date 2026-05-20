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
    public class CancelRequestCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<CancelRequestCommand, IResult>
    {
        public async Task<IResult> Handle(CancelRequestCommand request, CancellationToken cancellationToken)
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
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "You cannot cancel this request.");
            }

            if (sponsorshipRequest.Status != SponsorshipRequestStatus.Draft && sponsorshipRequest.Status != SponsorshipRequestStatus.PendingManagerApproval)
            {
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only draft or pending manager approval requests can be cancelled.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.Cancelled;

            sponsorshipRequest.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Cancelled",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .Cancelled,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(
                cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Cancelled Successfully.");
        }
    }
}
