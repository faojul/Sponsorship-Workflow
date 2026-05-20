using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Application.SponsorshipTypes.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipTypes.Queries
{
    // Query for All
    public record GetAllSponsorshipTypesQuery() : IRequest<Result<IReadOnlyList<SponsorshipTypeDto>>>;

    // Query by ID
    public record GetSponsorshipTypeByIdQuery(Guid Id) : IRequest<Result<SponsorshipTypeDto>>;

    // Unified Handler for Queries
    public class SponsorshipTypeQueryHandlers(IApplicationDbContext context)
                : IRequestHandler<GetAllSponsorshipTypesQuery, Result<IReadOnlyList<SponsorshipTypeDto>>>,
          IRequestHandler<GetSponsorshipTypeByIdQuery, Result<SponsorshipTypeDto>>
    {
        public async Task<Result<IReadOnlyList<SponsorshipTypeDto>>> Handle(GetAllSponsorshipTypesQuery request, CancellationToken cancellationToken)
        {
            var types= await context.SponsorshipTypes
                .AsNoTracking()
                .Select(x => new SponsorshipTypeDto(x.Id, x.Name))
                .ToListAsync(cancellationToken);

            return await Result<IReadOnlyList<SponsorshipTypeDto>>.SuccessAsync((int)HttpStatusCode.OK, types, "Sponsorship types retrieved successfully.");
        }

        public async Task<Result<SponsorshipTypeDto>> Handle(GetSponsorshipTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.SponsorshipTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(entity == null)
            {
                return await Result<SponsorshipTypeDto>.FailAsync((int)HttpStatusCode.NotFound, "Sponsorship type not found.");
            }

            return await Result<SponsorshipTypeDto>.SuccessAsync((int)HttpStatusCode.OK, new SponsorshipTypeDto(entity.Id, entity.Name), "Sponsorship type retrieved successfully.");
        }
    }
}
