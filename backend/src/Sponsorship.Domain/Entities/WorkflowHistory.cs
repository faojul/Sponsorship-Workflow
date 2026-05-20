using Sponsorship.Domain.Common;
using Sponsorship.Domain.Enums;

namespace Sponsorship.Domain.Entities
{
    public class WorkflowHistory : BaseEntity
    {
        public Guid SponsorshipRequestId { get; set; }

        public SponsorshipRequest SponsorshipRequest { get; set; } = default!;

        public SponsorshipRequestStatus PreviousStatus { get; set; }

        public SponsorshipRequestStatus NewStatus { get; set; }

        public string Action { get; set; } = default!;

        public string PerformedByUserId { get; set; } = default!;

        public string? Remarks { get; set; }

        public DateTime PerformedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
