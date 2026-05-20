using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Domain.Entities;
using Sponsorship.Infrastructure.Identity;
using System.Data;

namespace Sponsorship.Infrastructure.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SponsorshipRequest> SponsorshipRequests => Set<SponsorshipRequest>();

        public DbSet<SponsorshipType> SponsorshipTypes => Set<SponsorshipType>();

        public DbSet<WorkflowHistory> WorkflowHistories => Set<WorkflowHistory>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}
