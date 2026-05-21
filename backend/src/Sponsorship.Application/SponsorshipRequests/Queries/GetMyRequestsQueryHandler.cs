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
    public class GetMyRequestsQueryHandler(
        IApplicationDbContext context, ICurrentUserService currentUserService)
        : IRequestHandler<GetMyRequestsQuery,
        Result<List<SponsorshipRequestDto>>>
    {
        public async Task<Result<List<SponsorshipRequestDto>>> Handle(
            GetMyRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var items =
                await context.SponsorshipRequests
                    .AsNoTracking()
                    .Include(x => x.SponsorshipType)
                    .Where(x =>
                        x.RequestorId == currentUserService.UserId)
                    .OrderByDescending(x =>
                        x.UpdatedAtUtc)
                    .Select(x =>
                        new SponsorshipRequestDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Department = x.Department,
                            SponsorshipTypeName =x.SponsorshipType.Name,
                            EventName = x.EventName,
                            EventDate = x.EventDate.Date,
                            RequestedAmount =x.RequestedAmount,
                            Purpose = x.Purpose,
                            Status = x.Status.ToString(),
                            CreatedAtUtc = x.CreatedAtUtc
                        })
                    .ToListAsync(cancellationToken);

            return await Result<List<SponsorshipRequestDto>>.SuccessAsync((int)HttpStatusCode.OK,items);
        }
    }
}
