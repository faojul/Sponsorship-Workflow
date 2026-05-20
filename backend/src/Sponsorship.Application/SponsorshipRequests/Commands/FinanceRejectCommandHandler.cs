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
    public class FinanceRejectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<FinanceRejectCommand, IResult>
    {
        public async Task<IResult> Handle(FinanceRejectCommand request, CancellationToken cancellationToken)
        {
            var sponsorshipRequest =
                await context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            if (sponsorshipRequest is null)
            {
                return await Result.FailAsync((int)HttpStatusCode.NotFound, "Request not found.");
            }


            if (sponsorshipRequest.Status != SponsorshipRequestStatus.PendingFinanceReview)
            {
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only requests pending finance review can be rejected.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.Rejected;

            context.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Rejected By Finance",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .Rejected,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Rejected Successfully.");
        }
    }
}
