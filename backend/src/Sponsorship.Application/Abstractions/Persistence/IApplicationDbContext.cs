using Sponsorship.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sponsorship.Application.Abstractions.Persistence
{
    public interface IApplicationDbContext: IDbContext
    {
        DbSet<SponsorshipRequest> SponsorshipRequests { get; }
        DbSet<SponsorshipType> SponsorshipTypes { get; }
        DbSet<WorkflowHistory> WorkflowHistories { get; }
    }
}
