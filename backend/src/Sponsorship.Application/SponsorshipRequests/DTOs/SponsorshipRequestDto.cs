using Sponsorship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.DTOs
{
    public class SponsorshipRequestDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Department { get; set; } = default!;

        public string SponsorshipTypeName { get; set; } = default!;

        public string EventName { get; set; } = default!;

        public DateTime EventDate { get; set; }

        public decimal RequestedAmount { get; set; }

        public string Purpose { get; set; } = default!;

        public SponsorshipRequestStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
