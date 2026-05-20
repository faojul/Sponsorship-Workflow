using Sponsorship.Domain.Common;
using Sponsorship.Domain.Enums;

namespace Sponsorship.Domain.Entities
{
    public class SponsorshipRequest : BaseEntity
    {
        public string Title { get; set; } = default!;

        public string Department { get; set; } = default!;

        public Guid SponsorshipTypeId { get; set; }

        public SponsorshipType SponsorshipType { get; set; } = default!;

        public string EventName { get; set; } = default!;

        public DateTime EventDate { get; set; }

        public decimal RequestedAmount { get; set; }

        public string Purpose { get; set; } = default!;

        public string? ExpectedBusinessBenefit { get; set; }

        public string? Remarks { get; set; }

        public SponsorshipRequestStatus Status { get; set; }
            = SponsorshipRequestStatus.Draft;

        public string RequestorId { get; set; } = default!;

        public string? ManagerRemarks { get; set; }

        public string? FinanceRemarks { get; set; }

        public ICollection<WorkflowHistory> WorkflowHistories { get; set; }
            = new List<WorkflowHistory>();
    }
}
