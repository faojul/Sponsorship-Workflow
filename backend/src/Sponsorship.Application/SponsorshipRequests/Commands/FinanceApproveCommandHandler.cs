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
    public class FinanceApproveCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<FinanceApproveCommand, IResult>
    {
        public async Task<IResult> Handle(FinanceApproveCommand request, CancellationToken cancellationToken)
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
                return await Result.FailAsync((int)HttpStatusCode.Forbidden, "Only requests pending finance review can be approved.");
            }

            var previousStatus = sponsorshipRequest.Status;

            sponsorshipRequest.Status = SponsorshipRequestStatus.Approved;

            context.WorkflowHistories.Add(
                new WorkflowHistory
                {
                    Id = Guid.NewGuid(),

                    SponsorshipRequestId =
                        sponsorshipRequest.Id,

                    Action = "Approved By Finance",

                    PreviousStatus = previousStatus,

                    NewStatus =
                        SponsorshipRequestStatus
                            .Approved,

                    PerformedByUserId = currentUserService.UserId
                });

            await context.SaveChangesAsync(cancellationToken);

            return await Result.SuccessAsync((int)HttpStatusCode.OK, "Approved Successfully.");
        }
    }
}
