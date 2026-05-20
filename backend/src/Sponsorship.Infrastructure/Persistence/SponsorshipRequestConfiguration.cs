using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sponsorship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Infrastructure.Persistence
{
    public class SponsorshipRequestConfiguration: IEntityTypeConfiguration<SponsorshipRequest>
    {
        public void Configure(EntityTypeBuilder<SponsorshipRequest> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Department)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EventName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.RequestedAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Purpose)
                .HasMaxLength(2000);

            builder.HasOne(x => x.SponsorshipType)
                .WithMany()
                .HasForeignKey(x => x.SponsorshipTypeId);

            builder.HasMany(x => x.WorkflowHistories)
                .WithOne(x => x.SponsorshipRequest)
                .HasForeignKey(x => x.SponsorshipRequestId);
        }
    }
}
