using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using Sponsorship.Domain.Constants;
using Sponsorship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public class GetRequestsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService) : IRequestHandler<GetRequestsQuery, PaginatedResult<SponsorshipRequestDto>>
    {
        public async Task<PaginatedResult<SponsorshipRequestDto>>Handle(
                GetRequestsQuery request,
                CancellationToken cancellationToken)
        {
            var query =
                context.SponsorshipRequests
                    .AsNoTracking()
                    .Include(x => x.SponsorshipType)
                    .AsQueryable();

            var roles = currentUserService.Roles;

            // Role-based filtering
            if (roles.Contains(Roles.Manager))
            {
                query = query.Where(x =>
                    x.Status ==
                    SponsorshipRequestStatus
                        .PendingManagerApproval);
            }
            else if (roles.Contains(Roles.FinanceAdmin))
            {
                query = query.Where(x =>
                    x.Status ==
                    SponsorshipRequestStatus
                        .PendingFinanceReview);
            }

            // Status filtering
            if (!string.IsNullOrWhiteSpace(
                    request.Status)
                &&
                Enum.TryParse<
                    SponsorshipRequestStatus>(
                    request.Status,
                    true,
                    out var parsedStatus))
            {
                query = query.Where(x =>
                    x.Status == parsedStatus);
            }

            // Search filtering
            if (!string.IsNullOrWhiteSpace(
                    request.SearchTerm))
            {
                var search =
                    request.SearchTerm.Trim().ToLower();

                query = query.Where(x =>
                    x.Title.ToLower().Contains(search)
                    ||
                    x.EventName.ToLower().Contains(search)
                    ||
                    x.Department.ToLower()
                        .Contains(search));
            }

            var totalCount =
                await query.CountAsync(
                    cancellationToken);

            var items =
                await query
                    .OrderByDescending(x =>
                        x.CreatedAtUtc)
                    .Skip(
                        (request.PageNumber - 1)
                        * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x =>
                        new SponsorshipRequestDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Department = x.Department,
                            SponsorshipTypeName = x.SponsorshipType.Name,
                            EventName = x.EventName,
                            EventDate = x.EventDate.Date,
                            RequestedAmount = x.RequestedAmount,
                            Purpose = x.Purpose,
                            Status = x.Status.ToString(),
                            CreatedAtUtc = x.CreatedAtUtc
                        })
                    .ToListAsync(cancellationToken);

            return PaginatedResult<SponsorshipRequestDto>.Success((int)HttpStatusCode.OK, items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
