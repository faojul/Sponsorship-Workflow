using MediatR;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public record GetRequestsQuery(
        string? Status,
        string? SearchTerm,
        int PageNumber = 1,
        int PageSize = 10
        )
    : IRequest<PaginatedResult<SponsorshipRequestDto>>;
}
