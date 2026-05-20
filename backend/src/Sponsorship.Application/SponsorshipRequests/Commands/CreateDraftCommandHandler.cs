using MediatR;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Commands
{
    public class CreateDraftCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
        : IRequestHandler<CreateDraftCommand,
        Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
        {
            var entity = new SponsorshipRequest
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Department = request.Department,
                SponsorshipTypeId = request.SponsorshipTypeId,
                EventName = request.EventName,
                EventDate = request.EventDate,
                RequestedAmount = request.RequestedAmount,
                Purpose = request.Purpose,
                ExpectedBusinessBenefit = request.ExpectedBusinessBenefit,
                Remarks = request.Remarks,
                RequestorId = currentUserService.UserId
            };

            await context.SponsorshipRequests
                .AddAsync(entity, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync((int)HttpStatusCode.Created,entity.Id);
        }
    }
}
