using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipTypes.Commands
{
    public class UpdateSponsorshipTypeCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class UpdateSponsorshipTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSponsorshipTypeCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateSponsorshipTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.SponsorshipTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return await Result<bool>.FailAsync((int) HttpStatusCode.NotFound, "Sponsorship type not found.");
            }

            entity.Name = request.Name;
            entity.UpdatedAtUtc = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);
            return await Result<bool>.SuccessAsync((int) HttpStatusCode.OK, true, "Sponsorship type updated successfully.");
        }
    }
}
