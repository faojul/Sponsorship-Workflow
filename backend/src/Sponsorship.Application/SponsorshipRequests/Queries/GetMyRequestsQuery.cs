using MediatR;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipRequests.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Queries
{
    public record GetMyRequestsQuery()
    : IRequest<Result<List<SponsorshipRequestDto>>>;
}
