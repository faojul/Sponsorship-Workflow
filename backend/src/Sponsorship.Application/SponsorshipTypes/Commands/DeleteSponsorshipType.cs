using MediatR;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipTypes.Commands
{
    public record DeleteSponsorshipTypeCommand(Guid Id) : IRequest<Result<bool>>;

    public class DeleteSponsorshipTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSponsorshipTypeCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteSponsorshipTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.SponsorshipTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return await Result<bool>.FailAsync(404, "Sponsorship type not found.");
            }

            context.SponsorshipTypes.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return await Result<bool>.SuccessAsync(200, true, "Sponsorship type deleted successfully.");
        }
    }
}
