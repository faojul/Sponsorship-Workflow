using MediatR;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Application.Common.Results;
using Sponsorship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sponsorship.Application.SponsorshipTypes.Commands
{
    public record CreateSponsorshipTypeCommand(string Name) : IRequest<Result<Guid>>;

    public class CreateSponsorshipTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSponsorshipTypeCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSponsorshipTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new SponsorshipType { Name = request.Name };

            context.SponsorshipTypes.Add(entity);
            await context.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync((int)HttpStatusCode.Created, entity.Id, "Sponsorship type created successfully.");
        }
    }
}
